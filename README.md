## A*
### Dados do problema:
Suponha que queremos construir um sistema para auxiliar um usuário do metrô de Paris a saber o trajeto mais rápido entre a estação onde ele se encontra e a estação de destino. O usuário tem um painel com o mapa, podendo selecionar a sua estação de destino. O sistema então acende as luzes sobre o mapa mostrando o melhor trajeto a seguir (em termos de quais estações ele vai atravessar e quais as conexões mais rápidas a fazer – se for o caso). Para     facilitar a vida, consideramos apenas 4 linhas do metrô. <br><br>
Considere que:
- A distância em linha reta entre duas estações quaisquer é dada pela tabela 1 e a distância real é dada pela tabela 2.
- A velocidade média de um trem é de 30km/h;
- O tempo gasto para trocar de linha dentro de mesma estação (fazer baldeação) é de 4 minutos.


- Tabela1: distâncias diretas entre as estações do Metrô de Paris. <br> <br>
![image](https://user-images.githubusercontent.com/55666691/190299752-98b2f102-ecbe-4a52-bd7f-29eaef97911f.png)

- Tabela2: distâncias reais entre as estações do Metrô de Paris. <br><br>
![image](https://user-images.githubusercontent.com/55666691/190299910-73f07d1c-55d3-4a7a-9878-444d1d4b03e7.png)

- Mapa do metrô de Paris. <br><br>
![image](https://user-images.githubusercontent.com/55666691/190300003-d1b93a89-5d2c-4d7c-b4a6-0393b5a9ccb8.png)
