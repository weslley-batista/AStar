namespace CinAI
{
    using System.Collections.Generic;
    public class Graph{

        //Representa o grafo como uma lista de adjacência
        //Retorna a tupla (nº estação, cor da linha, distância direta)        
        List<(int,string,double)>[] connections = new List<(int,string,double)>[14];

        //Array que funciona como uma look-up table da dis. em linha reta de um nó para outro
        double [,] heuristics = new double [14,14];

        //Construtor
        public Graph(){
            //Inicializa a lista de adjacência
            for (int i = 0; i < connections.Length; i++){
                connections[i] = new List<(int,string,double)> ();
            }
        }

        //Adiciona uma conexão na lista de adjacência
        public void AddConncection(int origin, int destiny, string color, double weight){
            connections[origin-1].Add(((destiny-1), color, weight));
            connections[destiny-1].Add(((origin-1), color, weight));
        }
        
        //Retorna uma lista com as estações com qual a estação dada se conecta
        public List<int> GetStationConnections(int station){
            List<int> stationConnections = new List<int> ();
            foreach(var connection in connections[station]){
                stationConnections.Add(connection.Item1);
            }
            return stationConnections;
        }

        //Retorna a cor da linha que conecta duas estações
        public string GetColor(int current,int index){
            if(index == -1){
                return "";
            }
            return connections[current][index].Item2;
        }
                
        //Retorna o index do next na lista de adjacência do current
        public int GetNextIndex(int current, int next){
            return connections[current].FindIndex(t => t.Item1 == next);
        }

        //Retorna a distância direta entre as estações current e index
        public double GetWeight(int current, int index){
            return connections[current][index].Item3;
        }
        
        //Verifica se é necessário realizar baldeação indo de current para index estando em currentLine
        public int CheckTransfer(int current, int index, string currentLine){
            //Caso inicial, quando não se está em nenhuma linha
            if (currentLine == ""){
                return 0;
            }
            //Caso em que ambas não é necessário fazer baldeação
            else if(connections[current][index].Item2 == currentLine){
                return 0;
            }
            //Caso em que há baldeação
            else{
                return 2; //2 = distância que o metô andaria se não houvesse baldeação
            }
        }

        //Retorna a heurística levando em conta a distância em linha reta, a distância de conexão e a baldeação
        public double Heuristic(int current, int next, int index, int destiny,string currentLine){
            return heuristics[next,destiny] + GetWeight(current, index) + CheckTransfer(current, index,currentLine);
        }

        //Adicionando as conexões do Grafo
        public List<(int,string,double)>[] SetConnections(){
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
            AddConncection(13,14,"verde",5.1);

            return connections;
        }

        // Adiciona as heurísticas do grafo
        public double[,] SetHeurisitcs(){
            heuristics = new double [14,14] {
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
            return heuristics;
        }
    }
}