namespace Movies.Core.Querys
{
    public class QueryPaginationInfo
    {
        public int TotalDeElementos { get; set; }

        public int TamanhoDaPagina { get; set; }

        public int Numero { get; set; }

        public int TotalPaginas
        {
            get
            {
                if (TamanhoDaPagina != 0)
                {
                    return (int)Math.Ceiling((double)TotalDeElementos / (double)TamanhoDaPagina);
                }

                return 0;
            }
        }

        public int PrimeiraPagina => 0;

        public int UltimaPagina
        {
            get
            {
                if (TotalPaginas != 0)
                {
                    return TotalPaginas - 1;
                }

                return 0;
            }
        }

        public bool PossuiPaginaAnterior => Numero >= 1;

        public bool PossuiPaginaSeguinte => Numero < UltimaPagina;

        public int PaginaAnterior
        {
            get
            {
                if (PossuiPaginaAnterior)
                {
                    return Numero - 1;
                }

                return PrimeiraPagina;
            }
        }

        public int PaginaSeguinte
        {
            get
            {
                if (PossuiPaginaSeguinte)
                {
                    return Numero + 1;
                }

                return UltimaPagina;
            }
        }
    }
}
