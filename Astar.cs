namespace CinAI
{
    using System.Collections.Generic;
    public static class Astar{

        //Função que retorna o menor caminho usando o A*
        public static List<int> FindPath(Graph graph,int origin, int destiny,
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

                //Verifica a cor da linha atual
                if(current != origin){
                    currentLine = graph.GetColor(current, index);
                }

                //Se o nó é o destino, quebra o laço
                if(current == destiny){ 
                    break;
                }
                
                //Para cada nó da fronteira ...
                foreach(int next in graph.GetStationConnections(current)){
                    double nextCost = costs[current];

                    //Se o nó não está no dicionário de custos ou se ele o caminho atual leva até ele com um custo mais baixo
                    //muda seu custo associado e o adiciona a fronteira
                    if(!costs.ContainsKey(next) || nextCost < costs[next]){
                        costs[next] = nextCost;
                        int index = graph.GetNextIndex(current, next);
                        double newCost = nextCost + graph.Heuristic(current, next, index, destiny, currentLine);
                        Console.WriteLine((current + 1).ToString() + " "+ (next+1).ToString() + " " + graph.CheckTransfer(current, index, currentLine) + " " + currentLine);
                        frontier.Enqueue(next, newCost);
                        previous[next] = current;
                    }
                }
            }

            //Pega o caminho final e o custoa partir dos "pontos pais"
            List<int> path = new List<int>();
            int currNode = destiny;
            double totalCost = 0;

            while(currNode != origin){
                path.Add(currNode + 1);
                int index = graph.GetNextIndex(currNode, previous[currNode]);
                totalCost += graph.GetWeight(currNode, index);
                currNode = previous[currNode];
            }
            path.Add(origin + 1);
            path.Reverse(); //destiny -> origin  ---> origin -> destiny
            Console.WriteLine(totalCost);
            return path;
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
    }
}