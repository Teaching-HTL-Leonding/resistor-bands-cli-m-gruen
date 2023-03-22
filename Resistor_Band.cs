// resistor Band colors to their numeric values

Console.OutputEncoding = System.Text.Encoding.Default;

#region Constants
const int BLACK = 0;
const int BROWN = 1;
const int RED = 2;
const int ORANGE = 3;
const int YELLOW = 4;
const int GREEN = 5;
const int BLUE = 6;
const int VIOLET = 7;
const int GREY = 8;
const int WHITE = 9;

const int BLACK_MULTIPLIER = 1;
const int BROWN_MULTIPLIER = 10;
const int RED_MULTIPLIER = 100;
const int ORANGE_MULTIPLIER = 1_000;
const int YELLOW_MULTIPLIER = 10_000;
const int GREEN_MULTIPLIER = 100_000;
const int BLUE_MULTIPLIER = 1_000_000;
const int VIOLET_MULTIPLIER = 10_000_000;
const int GREY_MULTIPLIER = 100_000_000;
const int WHITE_MULTIPLIER = 1_000_000_000;
const double GOLD_MULTIPLIER = 0.1;
const double SILVER_MULTIPLIER = 0.01;

const double BROWN_TOLERANCE = 1;
const double RED_TOLERANCE = 2;
const double GREEN_TOLERANCE = 0.5;
const double BLUE_TOLERANCE = 0.25;
const double VIOLET_TOLERANCE = 0.1;
const double GREY_TOLERANCE = 0.05;
const double GOLD_TOLERANCE = 5;
const double SILVER_TOLERANCE = 10;
#endregion

#region Main Program
{
    if (args.Length >= 1)
    {
        string bands = args[0];
        double resistanceValue;
        double tolerance;

        if (bands.Split('-').Length < 4 || bands.Split('-').Length > 5)
        {
            System.Console.WriteLine("Invalid number of bands.");
            System.Console.WriteLine("The resistor must have 4 or 5 bands.");
            return;
        }
        else
        {
            // This is a try catch block. It will try to run the code in the try block.
            // If an exception is thrown, the catch block will run.
            // The exception is stored in the ex variable.
            try
            {
                DecodeColorBands(bands, out resistanceValue, out tolerance);
                string resistanceValueNew = resistanceValue.ToString("N2");

                Console.WriteLine($"Resistance value: {resistanceValueNew}Ω");
                Console.WriteLine($"Tolerance: ±{tolerance}%");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    else
    {
        Console.WriteLine("Please enter the resistor band colors. Use the commandline Parameters inputs");
    }
}
#endregion

#region Methods
bool TryConvertColorToDigit(string color, out int digit)
{
    digit = color switch
    {
        "Bla" => BLACK,
        "Bro" => BROWN,
        "Red" => RED,
        "Ora" => ORANGE,
        "Yel" => YELLOW,
        "Gre" => GREEN,
        "Blu" => BLUE,
        "Vio" => VIOLET,
        "Gry" => GREY,
        "Whi" => WHITE,
        _ => -1,
    };
    if (digit == -1) { return false; }
    else { return true; }
}

bool TryGetMultiplierFromColor(string color, out double multiplier)
{
    multiplier = color switch
    {
        "Bla" => BLACK_MULTIPLIER,
        "Bro" => BROWN_MULTIPLIER,
        "Red" => RED_MULTIPLIER,
        "Ora" => ORANGE_MULTIPLIER,
        "Yel" => YELLOW_MULTIPLIER,
        "Gre" => GREEN_MULTIPLIER,
        "Blu" => BLUE_MULTIPLIER,
        "Vio" => VIOLET_MULTIPLIER,
        "Gry" => GREY_MULTIPLIER,
        "Whi" => WHITE_MULTIPLIER,
        "Gol" => GOLD_MULTIPLIER,
        "Sil" => SILVER_MULTIPLIER,
        _ => -1,
    };
    if (multiplier == -1) { return false; }
    else { return true; }
}

bool TryGetToleranceFromColor(string color, out double tolerance)
{
    tolerance = color switch
    {
        "Bro" => BROWN_TOLERANCE,
        "Red" => RED_TOLERANCE,
        "Gre" => GREEN_TOLERANCE,
        "Blu" => BLUE_TOLERANCE,
        "Vio" => VIOLET_TOLERANCE,
        "Gry" => GREY_TOLERANCE,
        "Gol" => GOLD_TOLERANCE,
        "Sil" => SILVER_TOLERANCE,
        _ => -1,
    };
    if (tolerance == -1) { return false; }
    else { return true; }
}

void DecodeColorBands(string bands, out double resistanceValue, out double tolerance)
{
    int counter = 0;
    string[] bandsArray = bands.Split('-');

    if (!TryConvertColorToDigit(bandsArray[0], out int firstBand))
    {
        // This is a throw statement. It will throw an exception.
        // The exception is an ArgumentException.
        throw new ArgumentException("Invalid first band color.");
    }
    if (!TryConvertColorToDigit(bandsArray[1], out int secondBand))
    {
        throw new ArgumentException("Invalid second band color.");
    }

    int thirdBand = 0;
    if (bandsArray.Length > 4)
    {
        if (!TryConvertColorToDigit(bandsArray[2], out thirdBand))
        {
            throw new ArgumentException("Invalid third band color.");
        }
        counter++;
    }

    if (!TryGetMultiplierFromColor(bandsArray[2 + counter], out double multiplier))
    {
        throw new ArgumentException("Invalid multiplier band color.");
    }
    if (!TryGetToleranceFromColor(bandsArray[3 + counter], out double toleranceValue))
    {
        throw new ArgumentException("Invalid tolerance band color.");
    }

    if (bandsArray.Length > 4) 
    { 
        resistanceValue = Math.Round((firstBand * 100 + secondBand * 10 + thirdBand) * multiplier, 2); 
    }
    else 
    { 
        resistanceValue = Math.Round((firstBand * 10 + secondBand) * multiplier, 2); 
    }
    tolerance = toleranceValue;
}
#endregion
