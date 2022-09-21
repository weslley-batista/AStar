namespace AStar //faltando botar baldeaçao, testar e refatorar (bonus: interface)
{
    using System;
    using System.Collections.Generic;
    
    static class Program
    {
        static List<(int,string,double)>[] connections = new List<(int,string,double)>[14];
        static double[,] heuristics = new double[14,14];

        static void CreateGraph(){
            //e1 = estação atual 
            //e2 = estação destino
            //h = distancia[e1][e2]
            double [,] heuristics = new double [14,14] {
                {0,10,18.5,24.8,36.4,38.8,35.8,25.4,17.6,9.1,16.7,27.3,27.6,29.8}, //1
                {10,0,8.5, 14.8, 26.6, 29.1, 26.1, 17.3 ,10, 3.5, 15.5, 20.9, 19.1, 21.8}, //2
                {18.5,8.5,0,6.3,18.2,20.6,17.6,13.6,9.4,10.3,19.5,19.1,12.1,16.6}, //3
                {24.8,14.8,6.3,0,12, 14.4, 11.5, 12.4, 12.6, 16.7, 23.6, 18.6, 10.6, 15.4}, //4
                {36.4,26.6,18.2,12,0,3,2.4,19.4,23.3,28.2,34.2,24.8,14.5,17.9}, //5
                {38.8,29.1,20.6,14.4,3,0,3.3,22.3, 25.7, 30.3, 36.7, 27.6, 15.2, 18.2}, //6
                {35.8,26.1,17.6,11.5,2.4,3.3,0,20,23,27.3,34.2,25.7,12.4,15.6},//7
                {25.4, 17.3, 13.6, 12.4, 19.4, 22.3, 20, 0, 8.2, 20.3, 16.1, 6.4, 22.7, 27.6}, //8
                {17.6,10,9.4,12.6,23.3,25.7,23,8.2,0,13.5,11.2,10.9,21.2,26.6}, //9
                {9.1, 3.5, 10.3, 16.7, 28.2, 30.3, 27.3, 20.3, 13.5, 0, 17.6, 24.2, 18.7, 21.2}, //10
                {16.7,15.5,19.5,23.6,34.2,36.7,34.2,16.1,11.2,17.6,0,14.2,31.5,35.5}, //11 
                {27.3,20.9,19.1,18.6,24.8,27.6,25.7,6.4,10.9,24.2,14.2,0,28.8,33.6}, //12
                {27.6,19.1,12.1,10.6,14.5,15.2,12.4,22.7,21.2,18.7,31.5,28.8,0,5.1},//13
                {29.8,21.8,16.6,15.4,17.9,18.2,15.6,27.6,26.6,21.2,35.5,33.6,5.1,0}//14
            };

            for (int i = 0; i < connections.Length; i++)
            {
                connections[i] = new List<(int,string,double)> ();
            }

            //Adicionando conexões no Grafo
            AddConncection(1,2,"azul",10);
            AddConncection(2,3,"azul",8.5);
            AddConncection(2,9,"amarelo",10);
            AddConncection(2,10,"amarelo",3.5);
            AddConncection(3,4,"azul",6.3);
            AddConncection(3,9,"vermelho",9.4);
            AddConncection(3,13,"vermelho",18.7);
            AddConncection(4,5,"azul",13);
            AddConncection(4,8,"verde",15.3);
            AddConncection(4,13,"verde",12.8);
            AddConncection(5,6,"azul",3);
            AddConncection(5,7,"amarelo",2.4);
            AddConncection(5,8,"amarelo",30);
            AddConncection(8,9,"amarelo",9.6);
            AddConncection(8,12,"verde",6.4);
            AddConncection(9,11,"vermelho",12.2);
            AddConncection(3,14,"verde",5.1);
        }

        static void AddConncection(int origin, int destiny, string color, double weight){
            connections[origin-1].Add(((destiny-1), color, weight));
            connections[destiny-1].Add(((origin-1), color, weight));
        }
        
        static int TreatInput(string input){
            if(input.Length == 2){
                return (int.Parse(input[1].ToString()) - 1);
            } else {
                string stationTxt = input[1].ToString() + input[2].ToString();
                return (int.Parse(stationTxt) - 1);
            }
        }

        static List<int> GetStationConnections(int station){
            List<int> stationConnections = new List<int> ();
            foreach(var connection in connections[station]){
                stationConnections.Add(connection.Item1);
            }
            return stationConnections;
        }

        static string GetColor(int previous,int next){
            return connections[previous][next].Item2;
        }

        static int CheckTransfer(int current, int next, string currentLine){
            if (currentLine == ""){
                return 0;
            }
            else{
                if(connections[current][next].Item2 == currentLine){
                return 0;
                }
                else{
                    return 2;
                }
            }
        }

        static List<int> AStar(int origin, int destiny){
            
            PriorityQueue<int, double> frontier = new PriorityQueue<int, double>();
            Dictionary<int,int> previous = new Dictionary <int,int>();
            Dictionary<int,double> costs = new Dictionary <int,double>();

            //Adiciona o no de origem na fronteira e inicializando os dicionarios
            frontier.Enqueue(origin, 0);
            previous[origin] = origin;
            costs[origin] = 0;
            string currentLine = "";

            while(frontier.Count > 0){
                int current = frontier.Dequeue();
                
                if(current == destiny){ 
                    break;
                }
                
                foreach(int next in GetStationConnections(current)){
                    double nextCost = costs[current];
                    if(!costs.ContainsKey(next) || nextCost < costs[next]){
                        costs[next] = nextCost;
                        int index = connections[current].FindIndex(t => t.Item1 == next);
                        double newCost = nextCost + heuristics[next,destiny] + connections[current][index].Item3 + CheckTransfer(current, index,currentLine);
                        frontier.Enqueue(next, newCost);
                        previous[next] = current;
                        currentLine = GetColor(current, index);
                        
                    }
                }
                
            }

            //Pega o caminho final a partir dos "pontos pais"
            List<int> path = new List<int>();
            int currNode = destiny;

            while(currNode != origin){
                path.Add(currNode + 1);
                currNode = previous[currNode];
            }
            path.Add(origin + 1);
            path.Reverse(); //destiny -> origin  ---> origin -> destiny

            return path;
        }
        
        static void Main(string[] args){
            CreateGraph();
            int origin = TreatInput(Console.ReadLine());
            int destiny = TreatInput(Console.ReadLine());
            List <int> result = AStar(origin, destiny);
            
            System.Console.WriteLine(); //so pra separar os inputs da saida
            System.Console.WriteLine("Entrou na estação");
            foreach (var item in result)
            {
                System.Console.WriteLine("e"+item);
            }
        }
    }   
}
