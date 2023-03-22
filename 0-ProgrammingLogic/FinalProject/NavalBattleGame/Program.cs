using System;
using System.Text.RegularExpressions;

int[,] tabuleiroLogica = new int[10, 10];
string[,] tabuleiroVisual = new string[10, 10];

Random aleatorioI = new Random();
Random aleatorioJ = new Random();
Random definePosicao = new Random();

int submarino = 2;
int encouracado = 4;
int portaAvioes = 3;
var listaDePosicoes = new List<int>();
int horizontalVertical = definePosicao.Next(0, 2);
int qtdeDeEncouracados = 0;
//GERA PORTA AVIÕES


Console.Write("Quantos Porta Aviões Criar? ");
int numeroDePortaAvioesDefinidos = Convert.ToInt32(Console.ReadLine());

Console.Write("Quantos Encouraçados Criar? ");
int qtdeEncouracados = Convert.ToInt32(Console.ReadLine());

int numeroDePortaAvioesCriados = 0;

while (numeroDePortaAvioesCriados < numeroDePortaAvioesDefinidos)
{
    int aleatorioPortaAvioes = aleatorioI.Next(0, 5);
    if (!listaDePosicoes.Contains(aleatorioPortaAvioes))
    {
        if (horizontalVertical == 1) //Define a Construção Horizontal
        {
            for (int x = 1; x <= 5; x++) //Constrói o Porta-Aviões Horizontal
                tabuleiroLogica[aleatorioPortaAvioes + x, aleatorioPortaAvioes] = portaAvioes;
        }
        else if (horizontalVertical != 1) //Define a Construção Vertical
        {
            for (int x = 1; x <= 5; x++)  //Constrói o Porta-Aviões Vertical
                tabuleiroLogica[aleatorioPortaAvioes, aleatorioPortaAvioes + x] = portaAvioes;
                
        }
    }
    else numeroDePortaAvioesCriados--; //Se a seed já existir repete o código
    
    numeroDePortaAvioesCriados++;
    listaDePosicoes.Add(aleatorioPortaAvioes);
}


//GERA ENCOURAÇADOS
for (int i = 0; i <= qtdeDeEncouracados; i++)
{
    int aleatorioEncouracado = aleatorioI.Next(0, 6);
    bool repeat = true;
    do
    {
        aleatorioEncouracado = aleatorioI.Next(0, 6);
        horizontalVertical = definePosicao.Next(0, 2);

        for (int x = 0; x <= 4; x++)
        {
            //VERIFICA SE AO CRIAR O ENCOURAÇADO COLIDE EM ALGUMA ESTRUTURA CRIADA ANTERIORMENTE
            if (horizontalVertical == 1 && tabuleiroLogica[aleatorioEncouracado + x, aleatorioEncouracado] == 0)
            {
                repeat = false;
            }
        }
    }
    while (repeat);

    if (horizontalVertical == 1)
    {
        tabuleiroLogica[aleatorioEncouracado + 1, aleatorioEncouracado] = encouracado;
        tabuleiroLogica[aleatorioEncouracado + 2, aleatorioEncouracado] = encouracado;
        tabuleiroLogica[aleatorioEncouracado + 3, aleatorioEncouracado] = encouracado;
        tabuleiroLogica[aleatorioEncouracado + 4, aleatorioEncouracado] = encouracado;
    }
    else if (horizontalVertical != 1)
    {
        tabuleiroLogica[aleatorioEncouracado, aleatorioEncouracado + 1] = encouracado;
        tabuleiroLogica[aleatorioEncouracado, aleatorioEncouracado + 2] = encouracado;
        tabuleiroLogica[aleatorioEncouracado, aleatorioEncouracado + 3] = encouracado;
        tabuleiroLogica[aleatorioEncouracado, aleatorioEncouracado + 4] = encouracado;
    }

}

//GERA SUBMARINOS
for (int i = 0; i <= 4; i++)
{
    bool repeat = true;
    int aleatorioSubmarino1 = aleatorioI.Next(0,9);
    int aleatorioSubmarino2 = aleatorioJ.Next(0,9);
    do
    {
        //VERIFICA AO CRIAR O SUBMARINO SE IRÁ COLIDIR COM ALGUMA ESTRUTURA CRIADA ANTERIORMENTE
        if (tabuleiroLogica[aleatorioSubmarino1, aleatorioSubmarino2] != 0)
            repeat = false;
        else
        {
            tabuleiroLogica[aleatorioSubmarino1, aleatorioSubmarino2] = submarino;
        }
    } while (repeat == true);
}

//GERA tabuleiroLogica
for (int i = 0; i <= 9; i++)
{
    for (int j = 0; j <= 9; j++)
    {
        if (tabuleiroLogica[i, j] != submarino && tabuleiroLogica[i, j] != encouracado && tabuleiroLogica[i, j] != portaAvioes)
            tabuleiroLogica[i, j] = 1;
        //Console.Write(tabuleiroLogica[i, j] + " ");
    }
    //Console.WriteLine();
}


//Preenchendo Tabuleiro Visual
for (int i = 0; i <= 9; i++)
{
    for (int j = 0; j <= 9; j++)
    {
        if (tabuleiroLogica[i,j] != 1)
        {
            tabuleiroVisual[i, j] = "▩";
        }
        else tabuleiroVisual[i, j] = "□";
    }
}

//Jogo
int misseis=3;

bool venceu = false;
do
{
    Console.WriteLine("Mísseis Restantes: " + misseis+"\n");
    string coordenadaAtaque;
Console.WriteLine("   A B C D E F G H I J");
for (int i = 0; i <= 9; i++)
{
    Console.Write(i + "  ");
    for (int j = 0; j <= 9; j++)
    {
        Console.Write(tabuleiroVisual[i, j] + " ");
    }
    Console.WriteLine();
}
    Console.WriteLine("\nAonde Atacar? ");
    coordenadaAtaque = Console.ReadLine();

    Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
    Match result = re.Match(coordenadaAtaque);

    string letra = result.Groups[1].Value;
    string numero = result.Groups[2].Value;

    int X = Convert.ToInt32(numero);
    int Y = converteLetraEmNumero(letra);

    if (tabuleiroLogica[X,Y] == submarino)
    {
        Console.WriteLine("Acertou um Submarino!");
        tabuleiroLogica[X, Y] = 0;
        tabuleiroVisual[X, Y] = "▩";
    }
    if (tabuleiroLogica[X, Y] == encouracado)
    {
        Console.WriteLine("Acertou um Encouraçado!");
        tabuleiroLogica[X, Y] = 0;
        tabuleiroVisual[X, Y] = "▩";
    }
    if (tabuleiroLogica[X, Y] == portaAvioes)
    {
        Console.WriteLine("Acertou um Porta Aviões!");
        tabuleiroLogica[X, Y] = 0;
        tabuleiroVisual[X, Y] = "▩";
    }
    if (tabuleiroLogica[X, Y] == 1)
    {
        Console.WriteLine("ÁGUA!!!!");
        tabuleiroLogica[X, Y] = 0;
        tabuleiroVisual[X, Y] = "X";
    }
    misseis--;
    if (misseis == 0)
    {
        Console.WriteLine("VOCÊ PERDEU!!!");
        break;
    }
    Thread.Sleep(500);
    Console.Clear();
    venceu = verificaSeVenceu(tabuleiroLogica);
    Console.WriteLine(venceu);
} while (!venceu || !Esgotar(misseis));

if (venceu)
{
    Console.WriteLine("PARABÉNS VOCÊ GANHOU !!! UHUUUL");
    Console.ReadKey();
}

static int converteLetraEmNumero(string letra)
{
    int resultado=0;

    if (letra == "A")
        resultado = 0;
    if (letra == "B")
        resultado = 1;
    if (letra == "C")
        resultado = 2;
    if (letra == "D")
        resultado = 3;
    if (letra == "E")
        resultado = 4;
    if (letra == "F")
        resultado = 5;
    if (letra == "G")
        resultado = 6;
    if (letra == "H")
        resultado = 7;
    if (letra == "I")
        resultado = 8;
    if (letra == "J")
        resultado = 9;

    return resultado;
}

static bool Esgotar(int misseis)
{
    if (misseis == 0);
        return true;
}

static bool verificaSeVenceu(int[,] tabuleiro)
{
    int somaDoArray = tabuleiro.Cast<int>().Sum();
    if (somaDoArray == 81)
        return true;
    else
        return false;
}




