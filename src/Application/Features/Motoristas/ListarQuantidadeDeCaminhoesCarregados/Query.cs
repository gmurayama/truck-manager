﻿using System;

namespace TruckManager.Application.Features.Motoristas
{
    public partial class ListarQuantidadeDeCaminhoesCarregados
    {
        public class Query
        {
            public DateTime DataInicial { get; set; }

            public DateTime DataFinal { get; set; }
        }
    }
}
