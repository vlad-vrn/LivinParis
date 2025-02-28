// See https://aka.ms/new-console-template for more information

using System.Drawing;
using Graph;

Console.WriteLine("Hello, World!");
//List<Point> points = new List<Point>();
//List<Noeud<string>> noeuds = new List<Noeud<string>>();
//Graphe g1 = new Graphe<T>("titre", noeuds, points);

static void AfficherFichier()
{
    string[] lines = File.ReadAllLines("..\\..\\..\\soc-karate.mtx").Skip(24).ToArray();
    foreach (string line in lines)
    {
        Console.WriteLine(line);
        Console.ReadKey();
    }
}

//AfficherFichier();

Graphe<string> g1 = new Graphe<string>("g1") { Titre = "g1" };

g1.RemplirGraphe();


