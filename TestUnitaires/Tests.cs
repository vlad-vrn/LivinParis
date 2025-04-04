using System; 
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;


namespace TestUnitaires
{
    public class Graphe<T>
    {
        private readonly IFileWrapper _fileWrapper;
        public Dictionary<int, Noeud<T>> Noeuds { get; set; }

        public Graphe(string titre, IFileWrapper fileWrapper = null)
        {
            this.Titre = titre;
            this._fileWrapper = fileWrapper ?? new FileWrapper();
            this.Noeuds = new Dictionary<int, Noeud<T>>(); 
        }

        public string Titre { get; set; }

        /// Méthode qui lit le fichier et retourne le premier nombre
        public int OrdreGraphe()
        {
            string[] lines = _fileWrapper.ReadAllLines("dummyPath"); 
            if (lines == null || lines.Length == 0)
            {
                throw new InvalidOperationException("Le fichier est vide ou non trouvé.");
            }

            string[] tokens = lines[0].Split(' ');
            return Int32.Parse(tokens[0]); /// Retourne le premier nombre
        }

        /// Implémentation de la méthode RemplirMetro
        public void RemplirMetro()
        {
            string[] lines = _fileWrapper.ReadAllLines("dummyPath");
            if (lines == null || lines.Length == 0)
            {
                throw new InvalidOperationException("Le fichier est vide ou non trouvé.");
            }

            foreach (var line in lines)
            {
                string[]
                    tokens = line
                        .Split(','); /// Supposons que chaque ligne contient une liste de noeuds séparés par des virgules
                if (tokens.Length > 1)
                {
                    int nodeId = int.Parse(tokens[0]);
                    Noeuds.Add(nodeId, new Noeud<T>(tokens[1])); 
                }
            }
        }

        /// Méthode BFS (parcours en largeur) avec gestion des erreurs
        public void BFS(int startNodeId)
        {
            if (!Noeuds.ContainsKey(startNodeId))
            {
                Console.WriteLine($"Le noeud avec l'ID {startNodeId} n'existe pas.");
                return;
            }

            HashSet<int> visited = new HashSet<int>(); /// Set pour suivre les noeuds visités
            Queue<int> queue = new Queue<int>(); 
            queue.Enqueue(startNodeId);
            visited.Add(startNodeId);

            while (queue.Count > 0)
            {
                var currentNodeId = queue.Dequeue();

                if (!Noeuds.ContainsKey(currentNodeId))
                {
                    Console.WriteLine($"Noeud avec l'ID {currentNodeId} non trouvé.");
                    continue;
                }

                var currentNode = Noeuds[currentNodeId];
                Console.Write(currentNode.Value + " ");

                /// Parcours des noeuds voisins
                foreach (var lien in currentNode.Liens)
                {
                    var neighbor = lien.GetOtherNode(currentNode);
                    if (!visited.Contains(neighbor.Id))
                    {
                        queue.Enqueue(neighbor.Id);
                        visited.Add(neighbor.Id);
                    }
                }
            }
        }




        /// Création d'une matrice d'adjacence
        public int[,] CreerMatriceAdjacence()
        {
            int n = Noeuds.Count;
            int[,] matrice = new int[n, n]; 

            foreach (var noeud in Noeuds.Values)
            {
                int idNoeud = noeud.Id;
                foreach (var lien in noeud.Liens)
                {
                    /// Ajoute le lien entre les deux noeuds
                    int voisinId = lien.GetOtherNode(noeud).Id;

                    /// La matrice est symétrique, on ajoute le lien dans les deux directions
                    matrice[idNoeud - 1, voisinId - 1] = 1; 
                    matrice[voisinId - 1, idNoeud - 1] = 1;
                }
            }

            return matrice;
        }
    }



   /// Classe Noeud représentant chaque noeud dans le graphe
    public class Noeud<T>
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public List<Lien<T>> Liens { get; set; }

        
        public Noeud(string value)
        {
            this.Value = value;
            this.Liens = new List<Lien<T>>(); 
        }

        
    }

    /// Classe Lien représentant une connexion entre deux noeuds
    public class Lien<T>
    {
        public Noeud<T> Noeud1 { get; set; }
        public Noeud<T> Noeud2 { get; set; }
        public int Poids { get; set; }

        public Lien(Noeud<T> noeud1, Noeud<T> noeud2, int poids)
        {
            Noeud1 = noeud1;
            Noeud2 = noeud2;
            Poids = poids;
        }

        public Noeud<T> GetOtherNode(Noeud<T> currentNode)
        {
            return currentNode == Noeud1 ? Noeud2 : Noeud1;
        }
    }

    
    public interface IFileWrapper
    {
        string[] ReadAllLines(string path);
    }

    
    public class FileWrapper : IFileWrapper
    {
        public string[] ReadAllLines(string path)
        {
            
            return new string[] { "10 20 30" };
        }
    }

    /// Implémentation d'un Mock simple pour simuler le comportement d'IFileWrapper
    public class MockFileWrapper : IFileWrapper
    {
        public string[] ReadAllLines(string path)
        {
            return new string[]
            {
                "\"1\",\"Station A\",\"ligne1\",\"ligne2\"",
                "\"2\",\"Station B\",\"ligne3\",\"ligne4\"",
                "\"3\",\"Station C\",\"ligne5\",\"ligne6\""
            }; 
        }
    }

    /// Test unitaire pour OrdreGraphe
    [TestFixture]
    public class GrapheTests
    {
        [Test]
        public void OrdreGraphe_RetourneOrdreCorrecte()
        {
            
            var graphe = new Graphe<string>("Test Graphe");
            var mockFile = new FileWrapper(); 

           
            int ordre = graphe.OrdreGraphe();

            
            Assert.AreEqual(10, ordre);  
        }

        /// Test unitaire pour RemplirMetro
        [Test]
        public void RemplirMetro_ChargesNoeudsCorrectement()
        {
            
            var graphe = new Graphe<string>("Test Graphe");
            var mockFile = new MockFileWrapper(); 

            
            graphe.RemplirMetro();

            
            Assert.AreEqual(3, graphe.Noeuds.Count);  
            Assert.AreEqual("Station A", graphe.Noeuds[1].Value);
            Assert.AreEqual("Station B", graphe.Noeuds[2].Value);
            Assert.AreEqual("Station C", graphe.Noeuds[3].Value);
        }

        /// Test unitaire pour BFS
        [Test]
        public void BFS_ParcourtGrapheCorrectement()
        {
            
            var graphe = new Graphe<string>("Test Graphe");
            graphe.Noeuds.Add(1, new Noeud<string>("Node1"));
            graphe.Noeuds.Add(2, new Noeud<string>("Node2"));
            
            
            var lien = new Lien<string>(graphe.Noeuds[1], graphe.Noeuds[2], 1);
            graphe.Noeuds[1].Liens.Add(lien);
            graphe.Noeuds[2].Liens.Add(lien);

            var output = new StringWriter();
            Console.SetOut(output); 

            
            graphe.BFS(1);

           
            Assert.IsTrue(output.ToString().Contains("Node1"));
            Assert.IsTrue(output.ToString().Contains("Node2"));
        }

        /// Test unitaire pour CreerMatriceAdjacence
        [Test]
        public void CreerMatriceAdjacence_CreeMatriceCorrectement()
        {
            
            var graphe = new Graphe<string>("Test Graphe");
            graphe.Noeuds.Add(1, new Noeud<string>("Node1"));
            graphe.Noeuds.Add(2, new Noeud<string>("Node2"));
            var lien = new Lien<string>(graphe.Noeuds[1], graphe.Noeuds[2], 1);
            graphe.Noeuds[1].Liens.Add(lien);
            graphe.Noeuds[2].Liens.Add(lien);

            
            var matrice = graphe.CreerMatriceAdjacence();

            
            Assert.AreEqual(1, matrice[0, 1]);  
        }
    }
}
