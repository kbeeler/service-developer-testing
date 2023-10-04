namespace BugTrackingApi.ContractTests;

public class ResponseFromServer
{
    public string? Id { get; set; }
    public string User { get; set; }
    public string Software { get; set; }
    public Issue Issue { get; set; }
    public string Status { get; set; }
    public string Created { get; set; }
}

public record Issue
{
    public string Description { get; set; }
    public string Narrative { get; set; }
}



/*
 * {
  "id": "a-different-description-a",
  "user": "ITUStudent",
  "software": "Microsoft Excel",
  "issue": {
    "description": "A different Description",
    "narrative": "Dogs make me sneeze"
  },
  "status": "InTriage",
  "created": "2023-10-04T20:39:01.244476\u002B00:00"
}*/