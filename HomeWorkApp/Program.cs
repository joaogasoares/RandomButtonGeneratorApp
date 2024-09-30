using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading; 

namespace BrigaDeRua
{
    internal partial class Program
    {
        static void Main(string[] args)
        {
            // Chama a introdução para verificar se o jogador conhece o gênero
            bool isFamiliar = Intro.CheckingPlayerFamiliarityWithGenre();

            // Pausa de 2 segundos após o método RecommendButtonSmashingTactic() antes de mostrar os golpes
            Thread.Sleep(2000);

            // Se o jogador não tiver familiaridade com o jogo, ele recebe uma sequência de movimentos
            if (!isFamiliar)
            {
                BrigaDeRuaFight fight = new BrigaDeRuaFight();
                var playerMoves = fight.RandomMoveSet();

                Console.WriteLine("Seus golpes para essa partida:");
                foreach (var move in playerMoves)
                {
                    Console.WriteLine($"Attack: {move.Ataque.ToString()}, Defense: {move.Defesa.ToString()}");
                }
            }
            else
            {
                Console.WriteLine("Vamos começar a luta! Sem necessidade de rever os golpes.");
            }

            Console.ReadLine();
        }

        public abstract class Luta
        {
            protected List<FighterMovesList> moveList = new List<FighterMovesList>();
            protected List<FighterMovesList> randomMoves = new List<FighterMovesList>();

            protected void CreateMoveList()
            {
                moveList.Clear();

                for (int ataque = 0; ataque < 5; ataque++)
                {
                    for (int defesa = 0; defesa < 3; defesa++)
                    {
                        moveList.Add(new FighterMovesList { Ataque = (Ataques)ataque, Defesa = (Defesas)defesa });
                    }
                }
            }

            public virtual void ShuffleMoves()
            {
                var rnd = new Random();
                randomMoves = moveList.OrderBy(x => rnd.Next()).ToList();
            }

            public abstract List<FighterMovesList> RandomMoveSet();

            protected virtual FighterMovesList PickOneRandomMove()
            {
                FighterMovesList output = randomMoves.Take(1).First();
                randomMoves.Remove(output);
                return output;
            }
        }

        public class BrigaDeRuaFight : Luta
        {
            public BrigaDeRuaFight()
            {
                CreateMoveList();
                ShuffleMoves();
            }

            public override List<FighterMovesList> RandomMoveSet()
            {
                List<FighterMovesList> output = new List<FighterMovesList>();

                // O jogador recebe um conjunto de 3 golpes aleatórios para a partida
                for (int i = 0; i < 3; i++)
                {
                    output.Add(PickOneRandomMove());
                }

                return output;
            }
        }

        public static class Intro
        {
            public static bool CheckingPlayerFamiliarityWithGenre()
            {
                Console.WriteLine("Olá lutador. Tem familiaridade com jogos de porradaria?");
                string userOutput = Console.ReadLine();

                if (userOutput.ToLower() == "sim")
                {
                    Console.WriteLine("Dahora amigo. Faz teu jogo pra eu ver!");
                    return true; // O jogador está familiarizado com o jogo
                }
                else if (userOutput.ToLower() == "nao")
                {
                    RecommendButtonSmashingTactic();
                    return false; // O jogador não está familiarizado com o jogo
                }

                // Caso o jogador digite algo inválido interpreta-se como se ele não soubesse jogar
                Console.WriteLine("Resposta não reconhecida, vamos assumir que você precisa de ajuda.");
                RecommendButtonSmashingTactic();
                return false;
            }

            public static void RecommendButtonSmashingTactic()
            {
                Console.WriteLine("Relaxa meu abacatinho. Vou te ajudar com isso.");
                Console.WriteLine("Ignore tudo o que voce sabe e aperte todos os botões ao mesmo tempo!");
                Console.WriteLine();
            }
        }
    }
}
