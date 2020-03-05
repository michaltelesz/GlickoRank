using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlickoRank.Helpers
{
    public class GlickoHelper
    {
        private const float TAU = 0.5f;
        private const float EPSILON = 0.000001f;

        public static GlickoPlayer CalculateRank(GlickoPlayer player, GlickoResult[] results)
        {
            ComputedPlayer currentComputedPlayer = GetComputedPlayer(player);
            List<ComputedPlayer> computedPlayers = new List<ComputedPlayer>();
            foreach (GlickoResult result in results)
            {
                computedPlayers.Add(GetComputedPlayer(result.Player, currentComputedPlayer.Mi, result.Result));
            }
            float v = 0;
            float deltaPart = 0;
            foreach (ComputedPlayer computedPlayer in computedPlayers)
            {
                v += MathF.Pow(computedPlayer.GPhi, 2) * computedPlayer.E * (1 - computedPlayer.E);
                deltaPart += computedPlayer.GPhi * (computedPlayer.Sj - computedPlayer.E);
            }
            v = 1f / v;
            float delta = v * deltaPart;

            float a = MathF.Log(player.Volatility * player.Volatility);
            float A = a;
            float B = 0;
            if (delta * delta > MathF.Pow(currentComputedPlayer.Phi, 2) + v)
            {
                B = MathF.Log(delta * delta - MathF.Pow(currentComputedPlayer.Phi, 2) - v);
            }
            else
            {
                int k = 1;
                while (fX(a - k * TAU, delta, currentComputedPlayer.Phi, v, a, TAU) < 0)
                {
                    k++;
                }
                B = a - k * TAU;
            }
            float fA = fX(A, delta, currentComputedPlayer.Phi, v, a, TAU);
            float fB = fX(B, delta, currentComputedPlayer.Phi, v, a, TAU);

            while (MathF.Abs(B - A) > EPSILON)
            {
                float C = A + (A - B) * fA / (fB - fA);
                float fC = fX(C, delta, currentComputedPlayer.Phi, v, a, TAU);
                if (fC * fB < 0)
                {
                    A = B;
                    fA = fB;
                }
                else
                {
                    fA = fA / 2f;
                }
                B = C;
                fB = fC;
            }
            float sigmaPrim = MathF.Exp(A / 2f);

            float phiStar = MathF.Sqrt(MathF.Pow(currentComputedPlayer.Phi, 2) + sigmaPrim * sigmaPrim);
            float phiPrim = 1f / (MathF.Sqrt((1f / (phiStar * phiStar)) + (1f / v)));
            float miPrim = currentComputedPlayer.Mi + (phiPrim * phiPrim) * deltaPart;
            return new GlickoPlayer
            {
                Rating = 173.7178f * miPrim + 1500,
                RD = 173.7178f * phiPrim,
                Volatility = sigmaPrim
            };
        }

        private static ComputedPlayer GetComputedPlayer(GlickoPlayer player, float mi0 = 0, float result = 0)
        {
            float mij = (player.Rating - 1500) / 173.7178f;
            float phi = player.RD / 173.7178f;
            float gPhi = 1f / (MathF.Sqrt(1 + 3 * (phi * phi) / (MathF.PI * MathF.PI)));
            float e = 1f / (1 + MathF.Exp(-gPhi * (mi0 - mij)));
            return new ComputedPlayer
            {
                Mi = mij,
                Phi = phi,
                GPhi = gPhi,
                E = e,
                Sj = result
            };
        }

        private static float fX(float x, float delta, float phi, float v, float a, float tau)
        {
            return (MathF.Exp(x) * (delta * delta - phi * phi - v - MathF.Exp(x)) / (2 * MathF.Pow(phi * phi + v + MathF.Exp(x), 2))) - ((x - a) / (tau * tau));
        }

        private class ComputedPlayer
        {
            public float Mi { get; set; }
            public float Phi { get; set; }
            public float GPhi { get; set; }
            public float E { get; set; }
            public float Sj { get; set; }
        }
    }

    public class GlickoPlayer
    {
        /// <summary>
        /// Rating r
        /// </summary>
        public float Rating { get; set; }
        /// <summary>
        /// Rating Deviation RD
        /// </summary>
        public float RD { get; set; }
        /// <summary>
        /// Rating volatility Sigma
        /// </summary>
        public float Volatility { get; set; }
    }

    public class GlickoResult
    {
        public GlickoPlayer Player { get; set; }
        public float Result { get; set; }
    }
}
