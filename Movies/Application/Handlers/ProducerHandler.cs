using Movies.Application.Commands;
using Movies.Application.Models;
using Movies.Application.Queries;

namespace Movies.Application.Handlers
{
    public class ProducerHandler
    {
        private readonly List<Producer> _producers = new List<Producer>(); // Pode ser substituído por um repositório

        public void Handle(CreateProducerCommand command)
        {
            var producer = new Producer
            {
                Name = command.Producer,
                Interval = command.Interval,
                PreviousWin = command.PreviousWin,
                FollowingWin = command.FollowingWin
            };
            _producers.Add(producer);
        }

        public List<Producer> Handle(GetProducersQuery query)
        {
            // Filtra os produtores com base no tipo (min/max)
            return _producers; // Adapte para retornar os dados corretos
        }
    }
}
