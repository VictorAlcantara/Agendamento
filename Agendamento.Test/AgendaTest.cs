using Agendamento.API.Contracts.V1.Requests;
using Agendamento.API.Domain.Interfaces.Repositories;
using Agendamento.API.Domain.Interfaces.Services;
using Agendamento.API.Domain.Services;
using Agendamento.API.MappingProfiles;
using Agendamento.API.Validators;
using Agendamento.Mock.Repositories;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Agendamento.Test
{
    [TestClass]
    public class AgendaTest
    {
        private ISalaRepository salaRepository;
        private IAgendaRepository agendaRepository;
        private ISalaService salaService;
        private IAgendaService agendaService;
        private IMapper mapper;

        public AgendaTest()
        {
            salaRepository = new MockSalaRepository();
            agendaRepository = new MockAgendaRepository(salaRepository);
            salaService = new SalaService(salaRepository);
            agendaService = new AgendaService(agendaRepository);
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RequestToDomainProfile>();
            });

            mapper = mapperConfig.CreateMapper();
        }

        #region CreateAgendaRequestValidator

        [TestMethod]
        public void CreateAgendaRequestValidator_TituloNull_ValidationFail()
        {
            var request = new CreateAgendaRequest
            {
                Titulo = "",
                HorarioInicio = new DateTime(2019, 08, 14),
                HorarioFim = new DateTime(2019, 08, 15),
                SalaId = 1
            };

            var validator = new CreateAgendaRequestValidator(agendaService, salaService, mapper);
            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void CreateAgendaRequestValidator_HorarioInicioGreaterHorarioFim_ValidationFail()
        {
            var request = new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 08, 14),
                HorarioFim = new DateTime(2019, 08, 11),
                SalaId = 1
            };

            var validator = new CreateAgendaRequestValidator(agendaService, salaService, mapper);
            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void CreateAgendaRequestValidator_SalaDoesNotExist_ValidationFail()
        {
            var request = new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 08, 14),
                HorarioFim = new DateTime(2019, 08, 19),
                SalaId = -1
            };

            var validator = new CreateAgendaRequestValidator(agendaService, salaService, mapper);
            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void CreateAgendaRequestValidator_SalaHorarioCollision_ValidationFail()
        {
            var requests = new List<CreateAgendaRequest>();

            requests.Add(new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 10),
                HorarioFim = new DateTime(2019, 1, 14),
                SalaId = 1
            });

            requests.Add(new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 14),
                HorarioFim = new DateTime(2019, 1, 16),
                SalaId = 1
            });

            requests.Add(new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 18),
                HorarioFim = new DateTime(2019, 1, 22),
                SalaId = 1
            });

            requests.Add(new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 1),
                HorarioFim = new DateTime(2019, 2, 15),
                SalaId = 1
            });

            var validator = new CreateAgendaRequestValidator(agendaService, salaService, mapper);

            foreach (var item in requests)
                Assert.IsFalse(validator.Validate(item).IsValid);
        }

        [TestMethod]
        public void CreateAgendaRequestValidator_SalaHorarioNoCollision_ValidationOk()
        {
            var requests = new List<CreateAgendaRequest>();
            requests.Add(new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 5),
                HorarioFim = new DateTime(2019, 1, 11),
                SalaId = 1
            });

            requests.Add(new CreateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 21),
                HorarioFim = new DateTime(2019, 1, 25),
                SalaId = 1
            });

            var validator = new CreateAgendaRequestValidator(agendaService, salaService, mapper);
            foreach (var item in requests)
                Assert.IsTrue(validator.Validate(item).IsValid);
        }

        #endregion CreateAgendaRequestValidator

        #region UpdateAgendaRequestValidator

        [TestMethod]
        public void UpdateAgendaRequestValidator_TituloNull_ValidationFail()
        {
            var request = new UpdateAgendaRequest
            {
                Titulo = "",
                HorarioInicio = new DateTime(2019, 08, 14),
                HorarioFim = new DateTime(2019, 08, 15),
                SalaId = 1
            };

            var validator = new UpdateAgendaRequestValidator(agendaService, salaService, mapper);
            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void UpdateAgendaRequestValidator_HorarioInicioGreaterHorarioFim_ValidationFail()
        {
            var request = new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 08, 14),
                HorarioFim = new DateTime(2019, 08, 11),
                SalaId = 1
            };

            var validator = new UpdateAgendaRequestValidator(agendaService, salaService, mapper);
            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void UpdateAgendaRequestValidator_SalaDoesNotExist_ValidationFail()
        {
            var request = new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 08, 14),
                HorarioFim = new DateTime(2019, 08, 19),
                SalaId = -1
            };

            var validator = new UpdateAgendaRequestValidator(agendaService, salaService, mapper);
            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }

        [TestMethod]
        public void UpdateAgendaRequestValidator_SalaHorarioCollision_ValidationFail()
        {
            var requests = new List<UpdateAgendaRequest>();

            requests.Add(new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 10),
                HorarioFim = new DateTime(2019, 1, 14),
                SalaId = 1
            });

            requests.Add(new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 14),
                HorarioFim = new DateTime(2019, 1, 16),
                SalaId = 1
            });

            requests.Add(new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 18),
                HorarioFim = new DateTime(2019, 1, 22),
                SalaId = 1
            });

            requests.Add(new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 1),
                HorarioFim = new DateTime(2019, 2, 15),
                SalaId = 1
            });

            var validator = new UpdateAgendaRequestValidator(agendaService, salaService, mapper);
            foreach (var item in requests)
                Assert.IsFalse(validator.Validate(item).IsValid);
        }

        [TestMethod]
        public void UpdateAgendaRequestValidator_SalaHorarioNoCollision_ValidationOk()
        {
            var requests = new List<UpdateAgendaRequest>();

            requests.Add(new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 5),
                HorarioFim = new DateTime(2019, 1, 11),
                SalaId = 1
            });

            requests.Add(new UpdateAgendaRequest
            {
                Titulo = "Agendamento qualquer",
                HorarioInicio = new DateTime(2019, 1, 21),
                HorarioFim = new DateTime(2019, 1, 25),
                SalaId = 1
            });

            var validator = new UpdateAgendaRequestValidator(agendaService, salaService, mapper);
            foreach (var item in requests)
                Assert.IsTrue(validator.Validate(item).IsValid);
        }

        #endregion UpdateAgendaRequestValidator
    }
}
