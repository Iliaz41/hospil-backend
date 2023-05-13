﻿using Hospital.Models;

namespace Hospital.Dtos.HealthStatus_dto
{
    public class CreateHealthStatusDto
    {
        public DateTime? Date { get; set; }

        public string? Description { get; set; }

        public double? Temperature { get; set; }

        public int? PressureSyst { get; set; }

        public int? PressureDiast { get; set; }

        public int? Pulse { get; set; }

        public int? Index { get; set; }

        public long PatientId { get; set; }
    }
}
