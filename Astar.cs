namespace CinAI
{
    using System.Collections.Generic;
    public static class Astar{

        //Função que retorna o menor caminho usando o A*
        public static void FindPath(Graph graph,int origin, int destiny,
                                         double[,] heuristics,
                                         List<(int,string,double)>[] connections){
            
            PriorityQueue<int, double> frontier = new PriorityQueue<int, double>(); //A fronteira do A*
            Dictionary<int,int> previous = new Dictionary <int,int>();     //Dict com o nó a partir do qual se chegou a dado nó
            Dictionary<int,double> costs = new Dictionary <int,double>();  //Custo de cada nó

            //Adiciona o no de origem na fronteira, inicializa os dicionarios e seta a linha inicial como nula
            frontier.Enqueue(origin, 0);
            previous[origin] = origin;
            costs[origin] = 0;
            string currentLine = "";

            //Enquanto a fronteira não for vazia
            while(frontier.Count > 0){
                frontier = PrintPQueue(frontier);
                int current = frontier.Dequeue(); //Pega o nó com maior prioridade
                
                //Se o nó é o destino, quebra o laço
                if(current == destiny){ 
                    break;
                }

                int prevIndex = graph.GetNextIndex(current, previous[current]);
                currentLine = graph.GetColor(current, prevIndex);

                //Para cada nó da fronteira ...
                foreach(int next in graph.GetStationConnections(current)){
                    double nextCost = costs[current];

                    //Se o nó não está no dicionário de custos ou se ele o caminho atual leva até ele com um custo mais baixo
                    //muda seu custo associado e o adiciona a fronteira
                    if(!costs.ContainsKey(next) || nextCost < costs[next]){
                        costs[next] = nextCost;
                        int index = graph.GetNextIndex(current, next);
                        double newCost = nextCost + graph.Heuristic(current, next, index, destiny, currentLine);
                        frontier.Enqueue(next, newCost);
                        previous[next] = current;
        
                    }
                }
            }

            //Pega o caminho final e o custoa partir dos "pontos pais"
            List<int> path = new List<int>();
            int currNode = destiny;
            double totalCost = 0;
            currentLine = "";
            while(currNode != origin){
                path.Add(currNode + 1);
                int index = graph.GetNextIndex(currNode, previous[currNode]);
                totalCost += graph.GetWeight(currNode, index) + graph.CheckTransfer(currNode, index, currentLine);
                currentLine = graph.GetColor(currNode, index);
                currNode = previous[currNode];
            }
            path.Add(origin + 1);
            path.Reverse(); //destiny -> origin  ---> origin -> destiny
            
            PrintPath(path, destiny);
            System.Console.WriteLine();
            PrintCost(totalCost);
        }

        //Função utilitária para printar a fronteira. 
        //Obs: essa função destrói a estrutura original, mas retorna um backup com os mesmos dados que a original
        private static PriorityQueue<int, double> PrintPQueue(PriorityQueue<int, double> frontier){
            //PriorityQueue de backup que será retornada uma vez que a original será destruída
            PriorityQueue<int,double> bkp = new PriorityQueue<int,double>();

            string result = " ";
            int firstElemment; //elemento removido a cada iteração   
            double priority;   //prioridade do elemento removido a cada iteração

            //Pega o primeiro elemento da PriorityQueue e o adiciona a string que será impressa, além de salvá-lo no backup
            while(frontier.TryDequeue(out firstElemment, out priority)){
                bkp.Enqueue(firstElemment,priority);
                result += "("+ (firstElemment+1).ToString() + ", d = " + priority.ToString("0.##") + ") ";
            }
            
            //Printa a fronteira e retorna o backup
            Console.WriteLine("Fronteira:" + result);
            return bkp;
        }
        
        private static void PrintPath(List<int> path, int destiny){
            Console.WriteLine();

            string finalTxt =  "Caminho: ";
            foreach (int item in path)
            {
                finalTxt += "e" + item;
                if(item != destiny+1){
                    finalTxt += " -> ";
                }
            }
            System.Console.WriteLine(finalTxt);
        }

        private static void PrintCost(double cost){
            (double,double) minSec = GetTime(cost);
            if (minSec.Item2 == 0) {
                Console.WriteLine("Tempo Estimado: " + minSec.Item1 + " minutos");
            } else{
                Console.WriteLine("Tempo Estimado: " + minSec.Item1 + " minutos e " + minSec.Item2 + " segundo(s)");
            }
            
        }

        //Retorna o tempo em (minutos,segundos)
        private static (double,double) GetTime(double distance){
            //Metro se move a 30km/h 
            double time = 2 * distance;
            double minutes = Math.Truncate(time);
            double seconds = (time % 1) * 60;
            return (minutes,seconds);
        }
    }
}
