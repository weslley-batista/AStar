namespace AStar{
    using System.Collections.Generic;
    
    public class Station{
        public string Name {get; private set;}
        //                estacao  cor   peso
        public List<Tuple<Station,string,double>> edgeList {get; private set;};

        public Station(string name){
            Name = name;
        }

        public void addEdge (Station origin, Station destiny, string color, double weight){
            origin.edgeList.add(new Tuple(destiny, color, weight));
            destiny.edgeList.add(new Tuple(origin, color, weight));
        }
        
        public string edgeColor (Station origin, Station destiny){
            int index = origin.edgeList.Findindex(destiny)

                return origin.edgeList[index]
        }
    }
}