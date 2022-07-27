using Moq;
using PontoNet.Domain.Commands.FechamentosMes.CalcularFechamentoMes;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces;
using PontoNet.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace PontoNet.Test.Domain.Commands.FechamentosMes.CalcularFechamentoMes
{
    public class CalcularFechamentoMesCommandHandlerTest
    {
        private readonly CalcularFechamentoMesCommandHandler _handler;

        private readonly Mock<IFechamentoMesRepository> _fechamentoMesRepository;
        private readonly Mock<IRegistroRepository> _registroRepository;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CalcularFechamentoMesCommandHandlerTest()
        {
            _fechamentoMesRepository = new Mock<IFechamentoMesRepository>();
            _registroRepository = new Mock<IRegistroRepository>();
            _unitOfWork = new Mock<IUnitOfWork>();

            _handler = new CalcularFechamentoMesCommandHandler(_fechamentoMesRepository.Object, _registroRepository.Object, _unitOfWork.Object);
        }

        [Fact]
        public async void MesSemFechamentoESemAnteriorDeveCalcularCorretamente()
        {
            // arrange
            var mesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var mesAnterior = mesAtual.AddMonths(-1);

            _fechamentoMesRepository.Setup(s => s.IncluirAsync(It.IsAny<FechamentoMes>())).Verifiable();
            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(mesAnterior)).ReturnsAsync((FechamentoMes?)null);
            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(mesAtual)).ReturnsAsync((FechamentoMes?)null);
            _unitOfWork.Setup(s => s.SaveChangesAsync()).Verifiable();

            var dataHoje = DateTime.Now.Date;
            var dataOntem = dataHoje.AddDays(-1);

            var registrosMes = new List<Registro>()
            {
                new Registro(dataOntem, "09:30", "12:00"),
                new Registro(dataOntem, "13:30", "18:00"),
                new Registro(dataHoje, "08:30", "12:00"),
                new Registro(dataHoje, "13:30", "18:00")
            };

            _registroRepository.Setup(s => s.ListarRegistrosDoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosMes);

            // act
            var command = new CalcularFechamentoMesCommand(mesAtual);

            await _handler.Handle(command, new CancellationToken());

            // TODO: o cálculo das horas deveria ser uma service, para poder ser testado individualmente
            // TODO: como testar que o fechamento gerado ficou com 15 hs, sem retornar o mesmo no CommandHandler?

            // assert
            _fechamentoMesRepository.Verify(v => v.IncluirAsync(It.IsAny<FechamentoMes>()), Times.Once);
            _unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void MesSemFechamentoComAnteriorDeveCalcularCorretamente()
        {
            // arrange
            var mesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var mesAnterior = mesAtual.AddMonths(-1);

            _fechamentoMesRepository.Setup(s => s.IncluirAsync(It.IsAny<FechamentoMes>())).Verifiable();
            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(mesAnterior)).ReturnsAsync(new FechamentoMes(mesAnterior, 0, 3D));
            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(mesAtual)).ReturnsAsync((FechamentoMes?)null);
            _unitOfWork.Setup(s => s.SaveChangesAsync()).Verifiable();

            var dataHoje = DateTime.Now.Date;
            var dataOntem = dataHoje.AddDays(-1);

            var registrosMes = new List<Registro>()
            {
                new Registro(dataOntem, "09:30", "12:00"),
                new Registro(dataOntem, "13:30", "18:00"),
                new Registro(dataHoje, "08:30", "12:00"),
                new Registro(dataHoje, "13:30", "18:00")
            };

            _registroRepository.Setup(s => s.ListarRegistrosDoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosMes);

            // act
            var command = new CalcularFechamentoMesCommand(mesAtual);

            await _handler.Handle(command, new CancellationToken());

            // TODO: o cálculo das horas deveria ser uma service, para poder ser testado individualmente
            // TODO: como testar que o fechamento gerado ficou com 18 hs (3hrs + 15hrs), sem retornar o mesmo no CommandHandler?

            // assert
            _fechamentoMesRepository.Verify(v => v.IncluirAsync(It.IsAny<FechamentoMes>()), Times.Once);
            _unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async void MesComFechamentoComAnteriorDeveCalcularCorretamente()
        {
            // arrange
            var mesAtual = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var mesAnterior = mesAtual.AddMonths(-1);

            _fechamentoMesRepository.Setup(s => s.IncluirAsync(It.IsAny<FechamentoMes>())).Verifiable();
            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(mesAnterior)).ReturnsAsync(new FechamentoMes(mesAnterior, 0, 3D));
            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(mesAtual)).ReturnsAsync(new FechamentoMes(mesAtual, 3D, 0D));
            _unitOfWork.Setup(s => s.SaveChangesAsync()).Verifiable();

            var dataHoje = DateTime.Now.Date;
            var dataOntem = dataHoje.AddDays(-1);

            var registrosMes = new List<Registro>()
            {
                new Registro(dataOntem, "09:30", "12:00"),
                new Registro(dataOntem, "13:30", "18:00"),
                new Registro(dataHoje, "08:30", "12:00"),
                new Registro(dataHoje, "13:30", "18:00")
            };

            _registroRepository.Setup(s => s.ListarRegistrosDoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosMes);

            // act
            var command = new CalcularFechamentoMesCommand(mesAtual);

            await _handler.Handle(command, new CancellationToken());

            // TODO: o cálculo das horas deveria ser uma service, para poder ser testado individualmente
            // TODO: como testar que o fechamento gerado ficou com 18 hs (3hrs + 15hrs), sem retornar o mesmo no CommandHandler?

            // assert
            _fechamentoMesRepository.Verify(v => v.IncluirAsync(It.IsAny<FechamentoMes>()), Times.Never);
            _unitOfWork.Verify(v => v.SaveChangesAsync(), Times.Once);
        }
    }
}