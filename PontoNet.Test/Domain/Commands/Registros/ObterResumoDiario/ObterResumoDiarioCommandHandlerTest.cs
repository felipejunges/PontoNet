using Moq;
using PontoNet.Domain.Commands.Registros.ObterResumoDiario;
using PontoNet.Domain.Entities;
using PontoNet.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace PontoNet.Test.Domain.Commands.Registros.ObterResumoDiario
{
    public class ObterResumoDiarioCommandHandlerTest
    {
        private readonly ObterResumoDiarioCommandHandler _handler;

        private readonly Mock<IFechamentoMesRepository> _fechamentoMesRepository;
        private readonly Mock<IRegistroRepository> _registroRepository;

        public ObterResumoDiarioCommandHandlerTest()
        {
            _fechamentoMesRepository = new Mock<IFechamentoMesRepository>();
            _registroRepository = new Mock<IRegistroRepository>();

            _handler = new ObterResumoDiarioCommandHandler(_fechamentoMesRepository.Object, _registroRepository.Object);
        }

        [Fact]
        public async void Dia1SemFechamentoDeveSomarOk()
        {
            // arrange
            var registroHoje = new Registro(DateTime.Now.Date, "08:30", "12:00");

            var registrosData = new List<Registro>()
            {
                registroHoje
            };

            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(It.IsAny<DateTime>())).ReturnsAsync((FechamentoMes?)null);
            _registroRepository.Setup(s => s.ListarRegistrosDaDataAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosData);
            _registroRepository.Setup(s => s.ListarRegistrosDoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosData);

            // act
            var command = new ObterResumoDiarioCommand();
            var result = await _handler.Handle(command, new CancellationToken());

            // assert
            Assert.Equal(5D, result.RestanteData.TotalHours);
            Assert.Equal(5D, result.RestanteMes.TotalHours);
            Assert.Equal(5D, result.RestanteFinal.TotalHours);
        }

        [Fact]
        public async void Dia2ManhaComFechamentoDeveSomarOk()
        {
            // arrange
            var dataHoje = DateTime.Now.Date;

            var registrosData = new List<Registro>()
            {
                new Registro(dataHoje, "08:30", "12:00")
            };

            var saldoInicialMes = 0D;
            var saldoAtualMes = 1D;

            var fechamentoMes = new FechamentoMes(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), saldoInicialMes, saldoAtualMes);

            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(fechamentoMes);
            _registroRepository.Setup(s => s.ListarRegistrosDaDataAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosData);

            // act
            var command = new ObterResumoDiarioCommand();
            var result = await _handler.Handle(command, new CancellationToken());

            // assert
            Assert.Equal(5D, result.RestanteData.TotalHours);
            Assert.Equal(4D, result.RestanteMes.TotalHours);
            Assert.Equal(4D, result.RestanteFinal.TotalHours);
        }

        [Fact]
        public async void Dia2CompletoComFechamentoDeveSomarOk()
        {
            // arrange
            var dataHoje = DateTime.Now.Date;

            var registrosData = new List<Registro>()
            {
                new Registro(dataHoje, "08:30", "12:00"),
                new Registro(dataHoje, "13:00", "18:00")
            };

            var saldoInicialMes = 0D;
            var saldoAtualMes = 1D;

            var fechamentoMes = new FechamentoMes(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), saldoInicialMes, saldoAtualMes);

            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(fechamentoMes);
            _registroRepository.Setup(s => s.ListarRegistrosDaDataAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosData);

            // act
            var command = new ObterResumoDiarioCommand();
            var result = await _handler.Handle(command, new CancellationToken());

            // assert
            Assert.Equal(0D, result.RestanteData.TotalHours);
            Assert.Equal(-1D, result.RestanteMes.TotalHours);
            Assert.Equal(-1D, result.RestanteFinal.TotalHours);
        }

        [Fact]
        public async void Dia2ManhaComFechamentoComSaldoAnteriorDeveSomarOk()
        {
            // arrange
            var dataHoje = DateTime.Now.Date;

            var registrosData = new List<Registro>()
            {
                new Registro(dataHoje, "08:30", "12:00")
            };

            var saldoInicialMes = 2D;
            var saldoAtualMes = 1D;

            var fechamentoMes = new FechamentoMes(new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1), saldoInicialMes, saldoAtualMes);

            _fechamentoMesRepository.Setup(s => s.ObterFechamentoMesAsync(It.IsAny<DateTime>())).ReturnsAsync(fechamentoMes);
            _registroRepository.Setup(s => s.ListarRegistrosDaDataAsync(It.IsAny<DateTime>())).ReturnsAsync(registrosData);

            // act
            var command = new ObterResumoDiarioCommand();
            var result = await _handler.Handle(command, new CancellationToken());

            // assert
            Assert.Equal(5D, result.RestanteData.TotalHours); // restam 5 horas no dia
            Assert.Equal(4D, result.RestanteMes.TotalHours); // mês iniciou com 1hra positiva (5h - 1h)
            Assert.Equal(2D, result.RestanteFinal.TotalHours); // mês iniciou com 1hra positiva e tem 2hs de saldo (5h - 1h - 2h)
        }
    }
}