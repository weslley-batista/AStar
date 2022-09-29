namespace CinAI
{
    using System;
    using System.Collections.Generic;
    
    static class Program
    {
       //Função para obter o input no formato correto
        static int GetOrigin(){
            bool inputChecker = false;
            string input = "";
            while (!inputChecker){
                System.Console.Write("Insira a estação de origem: ");
                input = Console.ReadLine();
                inputChecker = CheckInput(input);
            }
            return TreatInput(input);
        }
        static int GetDestiny(){
            bool inputChecker = false;
            string input = "";
            while (!inputChecker){
                System.Console.Write("Insira a estação de destino: ");
                input = Console.ReadLine();
                inputChecker = CheckInput(input);
            }
            return TreatInput(input);
        }
       
        //Função para realizar o tratamento do input
        static bool CheckInput(string input){
            if ((input[0] == 'E' || input[0] == 'e') && input.Length > 1){
                if (input[1] < '1' || input[1] > '9' || input.Length > 3){
                        Console.WriteLine("Estação Inexistente. As estções são numeradas de 1 a 14");
                        return false;
                } else if (input.Length == 3) {
                    if (input[1] != '1' || input[2] > '4'){
                        Console.WriteLine("Estação Inexistente. As estções são numeradas de 1 a 14");
                        return false;
                    } else {
                        return true;
                    }
                } else {
                        return true;
                }
            }
            else { 
                Console.WriteLine("Entrada Invalida. As entradas devem estar no formato deste exemplo: E1");
                return false;
            }
        }

        //Função para extrair o índex da estação dado no input
        static int TreatInput(string input){
            if(input.Length == 2){
                return (int.Parse(input[1].ToString()) - 1);
            } else {
                string stationTxt = input[1].ToString() + input[2].ToString();
                return (int.Parse(stationTxt) - 1);
            }
        }    
        
        static void Main(string[] args){
            // Cria o grafo do metrô e extrai o valor de g e h de cada nó
            Graph graph = new Graph();
            var connections = graph.SetConnections();
            var heuristics = graph.SetHeurisitcs();

            //Recebe o input da estação de origem e da estação de destino
            int origin = GetOrigin();
            int destiny = GetDestiny();

            System.Console.WriteLine(); //separa os inputs da saida

            //Executa o A* para encontrar o melhor caminho entre as estações
            Astar.FindPath(graph, origin, destiny, heuristics, connections);
        }
    }   
}
