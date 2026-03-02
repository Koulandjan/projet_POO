using Projet_POO2;
using Spectre.Console;
using System.Globalization;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");

        AnsiConsole.Write(
            new FigletText("KNN Classifier")
                .Centered()
                .Color(Color.Cyan));

       // Choisir K le nombre de voisin proche 
        int k = AnsiConsole.Prompt(
            new TextPrompt<int>("[green]Entrez la valeur de K :[/]")
                .Validate(x =>
                {
                    if (x <= 0)
                        return ValidationResult.Error("[red]K doit être > 0[/]");
                    return ValidationResult.Success();
                }));

       // Appelle de la fonction du choix de la distance
        Distance distance = ChoisirDistance();

        // Remplir la liste de a partir du des dataset training et test
        List<Graine> training = Graine.ChargerDonnees("seeds_dataset_training.csv");
        List<Graine> test = Graine.ChargerDonnees("seeds_dataset_test.csv");

        //Initialiser le classifieur
        ClassifieurKNN knn = new ClassifieurKNN(k, distance, training);

        //initialiser la class qui d'evalution pour la matrice de confusion et l'exactitude
        EvaluationSystem eval = new EvaluationSystem();

        AnsiConsole.Status()
            .Start("Classification en cours...", ctx =>
            {
                eval.ConstruireMatrice(knn, test);
                Thread.Sleep(1000);
            });

        double exactitude = eval.CalculerExactitude();
        int[][] matrice = eval.Matrice();

        
        AnsiConsole.Write(new Rule("[green]Matrice de Confusion[/]").Centered());

        var table = new Table();
        table.Border = TableBorder.Rounded;

        table.AddColumn("[blue] [/]");
        table.AddColumn("[green]Kama[/]");
        table.AddColumn("[green]Rosa[/]");
        table.AddColumn("[green]Canadian[/]");

        table.AddRow("Kama", matrice[0][0].ToString(), matrice[0][1].ToString(), matrice[0][2].ToString());
        table.AddRow("Rosa", matrice[1][0].ToString(), matrice[1][1].ToString(), matrice[1][2].ToString());
        table.AddRow("Canadian", matrice[2][0].ToString(), matrice[2][1].ToString(), matrice[2][2].ToString());

        AnsiConsole.Write(table);

        AnsiConsole.MarkupLine($"\n[bold yellow]Exactitude : {exactitude:F2}%[/]");


       //Cette partie sert a classifier un nouveau grain avec l'entree utilisateur des parametres du ble
        AnsiConsole.Write(new Rule("[cyan]Test d'un nouveau grain[/]").Centered());

        string entrer = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[cyan]Voulez-vous classifier un nouveau grain ordinaire ?[/]")
                .AddChoices("Oui", "Non"));

        if (entrer == "Oui")
        {
            //Appelle de la fonction du choix de la distance
            Distance distanceChoisie = ChoisirDistance();

            AnsiConsole.MarkupLine("[blue]Entrez les paramètres du blé[/]");

            //instancier du ble de l'utilisateur
            Graine alpha = Graine.SaisirGraine();

            //Classifieur
            ClassifieurKNN nouveauKnn = new ClassifieurKNN(k, distanceChoisie, training);
            string classe = nouveauKnn.Classifieur(alpha);

            AnsiConsole.MarkupLine(
                $"[bold green]Avec une précision globale de {exactitude:F2}%[/]\n" +
                $"Votre blé est probablement de type : [yellow]{classe}[/]");

            //les donnees json du ble de l'tulisateur
            JsonDonnee etatBleOrdinaire = new JsonDonnee
            {
                parametres = new JsonDonnee.Parametres
                {
                    K = k,
                    Distance = distanceChoisie.GetType().Name,
                    DateExecution = DateTime.Now
                },
                datasets = new JsonDonnee.DatasetInfo
                {
                    nombreDonneeEntrainement = training.Count
                },
                evaluation = new JsonDonnee.Evaluation
                {
                    exactitude = exactitude
                }
            };

            SauvegarderJson(etatBleOrdinaire);
        }

        //les donnees du fichier Json
        JsonDonnee etat = new JsonDonnee
        {
            parametres = new JsonDonnee.Parametres
            {
                K = k,
                Distance = distance.GetType().Name,
                DateExecution = DateTime.Now
            },
            datasets = new JsonDonnee.DatasetInfo
            {
                nombreDonneeEntrainement = training.Count,
                nombreDonneeTest = test.Count
            },
            evaluation = new JsonDonnee.Evaluation
            {
                matriceConfusion = matrice,
                exactitude = exactitude
            }
        };

        SauvegarderJson(etat);

        AnsiConsole.MarkupLine("[grey]Appuyez sur une touche pour quitter...[/]");
        Console.ReadKey();
    }

    

    static Distance ChoisirDistance()
    {
        string choix = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[yellow]Choisissez la distance[/]")
                .AddChoices("Euclidienne", "Manhattan"));

        if (choix == "Euclidienne")
            return new DistanceEuclidienne();
        else
            return new DistanceManhatan();
    }

    static void SauvegarderJson(JsonDonnee etat)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(etat, options);
        File.WriteAllText("etat_global.json", json);

        AnsiConsole.MarkupLine("[green]✔ JSON sauvegardé avec succès ![/]");
    }
}