using System;
using System.Text.Json;
using System.Text.Json.Serialization;

public class PaymentBill
{
    // Поля
    public decimal PaymentPerDay { get; set; }
    public int NumberOfDays { get; set; }
    public decimal PenaltyPerDay { get; set; }
    public int DaysDelayed { get; set; }

    [JsonIgnore]
    public decimal TotalWithoutPenalty => PaymentPerDay * NumberOfDays;

    [JsonIgnore] 
    public decimal Penalty => PenaltyPerDay * DaysDelayed;

    [JsonIgnore]
    public decimal TotalAmount => TotalWithoutPenalty + Penalty;

    public static bool ShouldSerializeComputedFields { get; set; } = false;

    public string Serialize()
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true,
            DefaultIgnoreCondition = ShouldSerializeComputedFields ? JsonIgnoreCondition.Never : JsonIgnoreCondition.WhenWritingDefault
        };
        return JsonSerializer.Serialize(this, options);
    }

    public static PaymentBill Deserialize(string json)
    {
        return JsonSerializer.Deserialize<PaymentBill>(json);
    }
}