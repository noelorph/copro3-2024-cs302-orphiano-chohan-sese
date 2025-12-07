using System;
using System.Collections.Generic;
using System.Threading;
using Application;
using static Application.CharacterCreationController;
using static Information.CharacterFeature;
using MySql.Data.MySqlClient;
using static System.Collections.Specialized.BitVector32;
using System.Text;

namespace Information
{
    public class User
    {
        public string UserName { get; set; } = "";
        public string PassWord { get; set; } = "";

        public User() { }

        public User(string username, string password)
        {
            UserName = username;
            PassWord = password;
        }
    }


    public static class MenuManager
    {
        public static readonly List<string> logMenu = new List<string>()
        {
            "Log in",
            "Create an account",
            "EXIT"
        };

        public static readonly List<string> mainMenu = new List<string>()
        {
            "NEW GAME",
            "LOAD GAME",
            "CAMPAIGN",
            "CREDITS",
            "EXIT"
        };
    }

    public static class CharacterFeature
    {
        //REQUIRED
        public static readonly string[] Starters = { "MCU Universe", "Ninja Universe", "DC Universe", "Pirate Universe", "Human Universe", "Multiverse", "Demon Universe", "Soul Society Universe" };
        public static readonly string[] Races = { "Celestial", "Asguardian", "Alien", "Wizard", "Archer", "Rizal", "Avenger", "falsegod", "Human" };
        public static readonly string[] Genders = { "Male", "Female" };

        //CHARACTER APPEARANCE
        public static readonly string[] Faces = { "Nonchalant", "Angry", "Happy", "Neutral", "Faceless" };
        public static readonly string[] FaceShapes = { "Diamond", "Oval", "Heart", "Oblong", "Round", "Square", "Triangle" };
        public static readonly string[] FacialHairs = { "None", "Beard", "Mustach", "Goatee", "Bandholz" };
        public static readonly string[] HairStyles = { "Bald", "Fringe", "Semi-Bald", "Ponytail", "Anastole", "Mozart Style", "Classic", "90's", "Middle Part" };
        public static readonly string[] EyeShapes = { "Almond", "Hunter", "Round", "Monolid", "Double eye-lid" };
        public static readonly string[] EyeColors = { "Blue", "Brown", "Black", "Green", "Grey", "Red", "Violet", "Rainbow" };
        public static readonly string[] NoseShapes = { "Model", "Roman", "Nubian", "Straight", "Crooked", "Fleshy" };
        public static readonly string[] MouthOrLipShapes = { "Full Lip", "Narrow Lip", "Down-Turn Lip", "Heart-Shaped Lip" };
        public static readonly string[] EarShapes = { "Big", "Small", "Attached", "Pointed", "None" };
        public static readonly string[] BodyTypes = { "Hour Glass", "Apple", "Inverted Angle", "Pear", "Rectangle" };
        public static readonly string[] SkinTones = { "White", "Pale", "Black", "Grey", "Yellow", "Orange" };

        //OTHERS
        public static readonly string[] UpperClothings = { "Spider-Man suit", "BatMan Suit", "Loki Armor", "Vest/Armor", "Nanotech Suit", "Businessman Suit", "Naked" };
        public static readonly string[] Shoes = { "Black Leather Shoes", "Rapid Boots", "Crocs", "Slippers", "None" };
        public static readonly string[] PantsList = { "BatMan pants", "Spider-Man Pants", "Thor", "Doctor-Strange Pants" };
        public static readonly string[] Accessories = { "Cape", "Thunder Belt", "Masks", "Gloves", "Infinity Gauntlet", "OMNITRIX" };

        public static readonly string[] Inventories = { "Mjolnir", "Gun", "Frying Pan", "Chainsaw", "Axe", "Capt. America Shield", "Odin Gungnir", "Web-Shooters", "Double-Edged Sword", "Trident", "Sniper" };

        //Power and Stats

        public struct StatsPower
        {
            private string power;
            private int stats;

            public StatsPower(string power, int stats)
            {
                this.power = power;
                this.stats = stats;
            }

            public string Power
            {
                get => power;
            }

            public int Stats
            {
                get => stats;
            }

            public override string ToString()
            {
                return
                    "\n║  • Power: " + power +
                    "\n║  • Power stats: " + stats;
            }
        }

        public static readonly List<StatsPower> SuperPowers = new List<StatsPower>()
        {
            new StatsPower("Fire", 12000),
            new StatsPower("Telekinesis", 12000),
            new StatsPower("Super Strength", 10000),
            new StatsPower("Lightning", 12000),
            new StatsPower("Vision", 9000),
            new StatsPower("Time Control", 13000),
            new StatsPower("Teleportation", 10000),
            new StatsPower("Invisibility", 5000),
            new StatsPower("Water Control", 20000),
            new StatsPower("Air Control", 30000)
        };

        public struct YourFinalBoss
        {
            private string bossName;
            private int bossHp;
            private int bossStrength;

            public YourFinalBoss(string name, int hp, int strength)
            {
                bossName = name;
                bossHp = hp;
                bossStrength = strength;
            }

            public string BossName
            {
                get =>  bossName;
            }

            public int BossHp
            {
                get => bossHp;
            }

            public int BossStrength
            {
                get => bossStrength;
            }

            public override string ToString()
            {
                return
                    "\n║ • Final Boss Name: " + bossName +
                    "\n║ • Final Boss HP: " + bossHp +
                    "\n║ • Final Boss Strength: " + bossStrength;
            }
        }

        public static readonly List<YourFinalBoss> FinalBosses = new List<YourFinalBoss>()
        {
            new YourFinalBoss("Celestial", 25000, 25000),
            new YourFinalBoss("Cosmic", 35000, 15000),
            new YourFinalBoss("falsegod", 15000, 35000),
            new YourFinalBoss("Childhood Friend", 30000, 30000),
            new YourFinalBoss("Rival", 25000, 25000)
        };
    }

    public class Character
    {
        public Character() { }

        public string UserName { get; set; } = "";
        public string Title { get; set; } = "Untitled";
        public string StarterUniverse { get; set; } = "Not Selected";
        public string Race { get; set; } = "Not Selected";
        public string Gender { get; set; } = "Not Selected";

        public string Face { get; set; } = "Not Selected";
        public string FaceShape { get; set; } = "Not Selected";
        public string FacialHair { get; set; } = "Not Selected";
        public string HairStyle { get; set; } = "Not Selected";
        public string EyeShape { get; set; } = "Not Selected";
        public string EyeColor { get; set; } = "Not Selected";
        public string NoseShape { get; set; } = "Not Selected";
        public string MouthOrLip { get; set; } = "Not Selected";
        public string EarShape { get; set; } = "Not Selected";
        public string BodyTypes { get; set; } = "Not Selected";

        public string UpperClothing { get; set; } = "Not Selected";
        public string Pants { get; set; } = "Not Selected";
        public string Shoes { get; set; } = "Not Selected";
        public string Accessories { get; set; } = "Not Selected";

        public string SkinTone { get; set; } = "Not Selected";
        public string InventoryItem { get; set; } = "Not Selected";

        public int Agility { get; set; } = 0;
        public int Strength { get; set; } = 0;

        public StatsPower Power { get; set; }
        public YourFinalBoss FinalBoss { get; set; }

        public override string ToString()
        {
            string divider = new string('─', 50);

            return
                "╔══════════════════════════════════════════════════════╗\n" +
                "║                 CHARACTER PROFILE                    ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                $"║  Title: {Title,-40}     ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                "║                     APPEARANCE                       ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                $"║  • Universe:  {StarterUniverse,-35}    ║\n" +
                $"║  • Race:      {Race,-35}    ║\n" +
                $"║  • Gender:    {Gender,-35}    ║\n" +
                $"║  • Skin Tone: {SkinTone,-35}    ║\n" +
                "║                                                      ║\n" +
                $"║  Face:        {Face,-35}    ║\n" +
                $"║  Face Shape:  {FaceShape,-35}    ║\n" +
                $"║  Facial Hair: {FacialHair,-35}    ║\n" +
                $"║  Hair Style:  {HairStyle,-35}    ║\n" +
                $"║  Eye Shape:   {EyeShape,-35}    ║\n" +
                $"║  Eye Color:   {EyeColor,-35}    ║\n" +
                $"║  Nose Shape:  {NoseShape,-35}    ║\n" +
                $"║  Mouth/Lip:   {MouthOrLip,-35}    ║\n" +
                $"║  Ear Shape:   {EarShape,-35}    ║\n" +
                "║                                                      ║\n" +
                $"║  Body Shape:  {BodyTypes,-35}    ║\n" +
                $"║  Upper Wear:  {UpperClothing,-35}    ║\n" +
                $"║  Pants:       {Pants,-35}    ║\n" +
                $"║  Shoes:       {Shoes,-35}    ║\n" +
                $"║  Accessories: {Accessories,-35}    ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                "║                    ABILITIES                         ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                $"║  • Super Power: {Power,-34}                                ║\n" +
                $"║  • Strength:    {Strength + "/10",-34}   ║\n" +
                $"║  • Agility:     {Agility + "/10",-34}   ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                "║                    STORY                             ║\n" +
                "╠══════════════════════════════════════════════════════╣\n" +
                $"║ • Final Boss:   {FinalBoss,-34}                         ║\n" +
                "╚══════════════════════════════════════════════════════╝";
        }
    }   
}

namespace CharacterAndAccountDatabase
{
    using Information;
    using IOSystem;
    using static Information.CharacterFeature;

    public class DataBaseConnector
    {
        private static readonly string _connection = "Server=localhost;Port=3306;Database=EpicBattleInOdysseyDB;Uid=root;Pwd=09222138665:v;";

        public MySqlConnection GetConnection() => new MySqlConnection(_connection);
    }

    public interface IAccountRepository
    {
        bool AddUser(User user);
        bool Validate(string username, string password);
        bool UserExists(string username);
    }

    public interface ICharacterRepository
    {
        void SaveCharacter(string username, Character character);
        Character LoadCharacter(string username, string title);
        bool CharacterExists(string username, string title);
        void DeleteCharacter(string username, string title);
    }

    public class AccountData : IAccountRepository
    {
        private readonly DataBaseConnector dbMySql = new DataBaseConnector();

        public bool AddUser(User user)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "INSERT INTO accounts (username, password) VALUES (@Username, @Password)";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", user.UserName);
            cmd.Parameters.AddWithValue("@Password", user.PassWord);

            /*Username is our PRIMARY KEY so username must be unique
            or else it will fail to execute in the database and will return false*/
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Validate(string username, string password)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE username = @UserName AND password = @Password";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@UserName", username);
            cmd.Parameters.AddWithValue("@Password", password);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public bool UserExists(string username)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "SELECT COUNT(*) FROM accounts WHERE username = @UserName";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }
    }

    public class CharacterData : ICharacterRepository
    {
        private readonly DataBaseConnector dbMySql;
        private readonly MenuSystem _system;

        public CharacterData(MenuSystem system)
        {
            dbMySql = new DataBaseConnector();
            _system = system;
        }

        public void SaveCharacter(string username, Character character)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "INSERT INTO characters" +
                "(username, title, starter_universe, race, gender, face, " +
                "face_shape, facial_hair, hair_style, eye_shape, eye_color, nose_shape, mouth_or_lip, ear_shape, " +
                "body_shape, upper_clothing, pants, shoes, accessories, skin_tone, inventory_item, super_power, " +
                "power_stat, strength, agility, Boss_name, Boss_hp, Boss_strength) " +
                "VALUES " +
                "(@Username, @Title, @Universe, @Race, @Gender, @Face, @FaceShape, @FacialHair, @HairStyle, @EyeShape, " +
                "@EyeColor, @NoseShape, @MouthOrLip, @EarShape, @Body, @UpperClothing, @Pants, @Shoes, @Accessories, @SkinTone, @Inventory, @SuperPower, " +
                "@PowerStat ,@Strength, @Agility, @BossName, @BossHp, @BossStrength)";

            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Title", character.Title);
            cmd.Parameters.AddWithValue("@Universe", character.StarterUniverse);
            cmd.Parameters.AddWithValue("@Race", character.Race);
            cmd.Parameters.AddWithValue("@Gender", character.Gender);
            cmd.Parameters.AddWithValue("@Face", character.Face);
            cmd.Parameters.AddWithValue("@FaceShape", character.FaceShape);
            cmd.Parameters.AddWithValue("@FacialHair", character.FacialHair);
            cmd.Parameters.AddWithValue("@HairStyle", character.HairStyle);
            cmd.Parameters.AddWithValue("@EyeShape", character.EyeShape);
            cmd.Parameters.AddWithValue("@EyeColor", character.EyeColor);
            cmd.Parameters.AddWithValue("@NoseShape", character.NoseShape);
            cmd.Parameters.AddWithValue("@MouthOrLip", character.MouthOrLip);
            cmd.Parameters.AddWithValue("@EarShape", character.EarShape);
            cmd.Parameters.AddWithValue("@Body", character.BodyTypes);
            cmd.Parameters.AddWithValue("@UpperClothing", character.UpperClothing);
            cmd.Parameters.AddWithValue("@Pants", character.Pants);
            cmd.Parameters.AddWithValue("@Shoes", character.Shoes);
            cmd.Parameters.AddWithValue("@Accessories", character.Accessories);
            cmd.Parameters.AddWithValue("@SkinTone", character.SkinTone);
            cmd.Parameters.AddWithValue("@Inventory", character.InventoryItem);
            cmd.Parameters.AddWithValue("@SuperPower", character.Power.Power);
            cmd.Parameters.AddWithValue("@PowerStat", character.Power.Stats);
            cmd.Parameters.AddWithValue("@Strength", character.Strength);
            cmd.Parameters.AddWithValue("@Agility", character.Agility);
            cmd.Parameters.AddWithValue("@BossName", character.FinalBoss.BossName);
            cmd.Parameters.AddWithValue("@BossHp", character.FinalBoss.BossHp);
            cmd.Parameters.AddWithValue("@BossStrength", character.FinalBoss.BossStrength);

            cmd.ExecuteNonQuery();
        }

        public Character LoadCharacter(string username, string title)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "SELECT * FROM characters WHERE username = @Username AND title = @Title LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Title", title);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Character
                {
                    UserName = reader["username"].ToString()?.Trim() ?? "",
                    Title = reader["title"].ToString()?.Trim() ?? "",
                    StarterUniverse = reader["starter_universe"].ToString()?.Trim() ?? "",
                    Race = reader["race"].ToString()?.Trim() ?? "",
                    Gender = reader["gender"].ToString()?.Trim() ?? "",
                    Face = reader["face"].ToString()?.Trim() ?? "",
                    FaceShape = reader["face_shape"].ToString()?.Trim() ?? "",
                    FacialHair = reader["facial_hair"].ToString()?.Trim() ?? "",
                    HairStyle = reader["hair_style"].ToString()?.Trim() ?? "",
                    EyeShape = reader["eye_shape"].ToString()?.Trim() ?? "",
                    EyeColor = reader["eye_color"].ToString()?.Trim() ?? "",
                    NoseShape = reader["nose_shape"].ToString()?.Trim() ?? "",
                    MouthOrLip = reader["mouth_or_lip"].ToString()?.Trim() ?? "",
                    EarShape = reader["ear_shape"].ToString()?.Trim() ?? "",
                    BodyTypes = reader["body_shape"].ToString()?.Trim() ?? "",
                    UpperClothing = reader["upper_clothing"].ToString()?.Trim() ?? "",
                    Pants = reader["pants"].ToString()?.Trim() ?? "",
                    Shoes = reader["shoes"].ToString()?.Trim() ?? "",
                    Accessories = reader["accessories"].ToString()?.Trim() ?? "",
                    SkinTone = reader["skin_tone"].ToString()?.Trim() ?? "",
                    InventoryItem = reader["inventory_item"].ToString()?.Trim() ?? "",
                    Power = new StatsPower(reader["super_power"].ToString()?.Trim() ?? "", Convert.ToInt32(reader["power_stat"])),
                    Strength = Convert.ToInt32(reader["strength"]),
                    Agility = Convert.ToInt32(reader["agility"]),
                    FinalBoss = new YourFinalBoss(reader["Boss_name"].ToString()?.Trim()??"", Convert.ToInt32(reader["Boss_hp"]), Convert.ToInt32(reader["Boss_strength"]))
                };
            }
            return new Character(); 
        }

        public bool CharacterExists(string username, string title)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "SELECT COUNT(*) FROM characters WHERE username = @Username AND title = @Title";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Title", title);

            return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
        }

        public void DeleteCharacter(string username, string title)
        {
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "DELETE FROM characters WHERE username = @Username AND title = @Title LIMIT 1";
            MySqlCommand cmd = new MySqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@Username", username);
            cmd.Parameters.AddWithValue("@Title", title);

            int affectedRows = cmd.ExecuteNonQuery();
            _system.DisplayMessage("Character: \"" + title + "\" deleted.", true);
            _system.DisplayMessage("No. of Rows affected in the database: " + affectedRows);
        }
    }

    public class LogSystem
    {
        private AccountData account;

        public enum AuthenticationResult
        {
            Success,
            InvalidCredentials,
            UserExists,
            TooManyAttempts
        }

        public LogSystem(AccountData account) { this.account = account; }

        public AuthenticationResult CreateAccount(string username, string password)
        {
            if (account.UserExists(username))
                return AuthenticationResult.UserExists;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return AuthenticationResult.InvalidCredentials;

            return account.AddUser(new User(username, password)) ? AuthenticationResult.Success : AuthenticationResult.UserExists;
        }

        public AuthenticationResult LogIn(string username, string password, byte attempts)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return AuthenticationResult.InvalidCredentials;
            if (attempts == 0)
                return AuthenticationResult.TooManyAttempts;

            return account.Validate(username, password) ? AuthenticationResult.Success : AuthenticationResult.InvalidCredentials;
        }
    }

    public class CharacterSave
    {
        private readonly CharacterData saveData;

        public CharacterSave(CharacterData saveData) { this.saveData = saveData; }

        public void SaveCharacter(string username, Character character)
        {
            saveData.SaveCharacter(username, character);
        }

        public Character LoadCharacter(string username, string title)
        {
            return saveData.LoadCharacter(username, title);
        }

        public bool CharacterExists(string username, string title)
        {
            return saveData.CharacterExists(username, title);
        }

        public void DeleteCharacter(string username, string title)
        {
            saveData.DeleteCharacter(username, title);
        }
    }

    public class CharacterBuilder
    {
        private readonly Character _character = new Character();

        public CharacterBuilder() { }

        public CharacterBuilder SetBasicInfo(string universe, string race)
        {
            _character.StarterUniverse = universe;
            _character.Race = race;
            return this;
        }

        public CharacterBuilder SetGender(string gender)
        {
            if(gender == "Male")
            {
                _character.Strength += 5000;
                _character.Agility += 2500;
            }
            else if(gender == "Female")
            {
                _character.Agility += 5000;
                _character.Strength += 2500;
            }

            _character.Gender = gender;
            return this;
        }

        public static string GenderCondition()
        {
            return "\nMale: +5000 Strength and +2500 Agility \nFemale: +5000 Agility and +2500 Strength\n";
        }

        public CharacterBuilder SetAppearance(string face, string faceShape, string facialHair, string hairStyle)
        {
            _character.Face = face;
            _character.FaceShape = faceShape;
            _character.FacialHair = facialHair;
            _character.HairStyle = hairStyle;
            return this;
        }

        public CharacterBuilder SetEyes(string eyeShape, string eyeColor)
        {
            _character.EyeShape = eyeShape;
            _character.EyeColor = eyeColor;
            return this;
        }

        public CharacterBuilder SetFeatures(string noseShape, string mouthOrLip, string earShape, string bodyShape)
        {
            _character.NoseShape = noseShape;
            _character.MouthOrLip = mouthOrLip;
            _character.EarShape = earShape;
            _character.BodyTypes = bodyShape;
            return this;
        }

        public CharacterBuilder SetClothing(string upperClothing, string pants, string shoes, string accessories)
        {
            _character.UpperClothing = upperClothing;
            _character.Pants = pants;
            _character.Shoes = shoes;
            _character.Accessories = accessories;
            return this;
        }

        public CharacterBuilder SetAttributes(string skinTone, string inventory)
        {
            _character.SkinTone = skinTone;
            _character.InventoryItem = inventory;
            return this;
        }

        public CharacterBuilder SetSuperPower(StatsPower power)
        {
            _character.Power = power;
            return this;
        }

        public CharacterBuilder SetStats(int agility, int strength)
        {
            _character.Agility = agility;
            _character.Strength = strength;
            return this;
        }

        public CharacterBuilder SetBoss(YourFinalBoss boss)
        {
            _character.FinalBoss = boss;
            return this;
        }

        public Character Build() => _character;
    }
}

namespace IOSystem
{
    public abstract class IInputService
    {
        public abstract string ReadLine();
    }

    public abstract class IOutputService
    {
        public abstract void WriteLine(string message);
        public abstract void Write(string message);
    }

    public class InputService : IInputService
    {
        public override string ReadLine() => Console.ReadLine()?.Trim() ?? "";
    }

    public class OutputService : IOutputService
    {
        public override void WriteLine(string message) { Console.WriteLine(message); }
        public override void Write(string message) { Console.Write(message); }
        public void Clear(int clearingTime = 200)
        {
            Thread.Sleep(clearingTime);
            Console.Clear();
        }
    }

    public class MenuSystem
    {
        private readonly OutputService _output;

        public MenuSystem(OutputService output)
        {
            _output = output;
        }

        public void DisplayMenu<T>(List<T> options, string prompt = "Choose an option")
        {
            _output.WriteLine(prompt);
            for (int i = 0; i < options.Count; i++)
            {
                _output.WriteLine((i + 1) + ". " + options[i]);
            }
            _output.Write("Input: ");
        }

        public void DisplayMessage(string message, bool isError = false)
        {
            var color = isError ? ConsoleColor.Red : ConsoleColor.Green;
            Console.ForegroundColor = color;
            _output.WriteLine(message);
            Console.ResetColor();
        }
    }

    public class MenuNavigator
    {
        private readonly MenuSystem _system;
        private readonly OutputService _output;
        private readonly InputService _input;

        public MenuNavigator(InputService input, OutputService output)
        {
            _input = input;
            _output = output;
            _system = new MenuSystem(output);
        }

        public byte GetMenuChoice<T>(List<T> options, string title, string user)
        {
            byte choice = 0;
            while (true)
            {
                _output.Clear(50);
                _system.DisplayMessage(user);
                _system.DisplayMessage(title + "\n");
                _system.DisplayMenu(options, "Choose an option");

                try
                {
                    choice = Convert.ToByte(_input.ReadLine());
                    if(choice >= 0 && choice <= options.Count)
                    {
                        return choice;
                    }
                }
                catch (FormatException e)
                {
                    _system.DisplayMessage("Invalid Format ERROR: [" + e.Message + "].", true);
                    _output.Clear(1000);
                    continue;
                }
                catch(OverflowException e)
                {
                    _system.DisplayMessage("Invalid ERROR: [" + e.Message + "].", true);
                    _output.Clear(1000);
                    continue;
                }

                _system.DisplayMessage("Invalid input. Please try again.");
                _output.Clear(1000);
            }
        }

        public string GetValidatedInput(string prompt, Func<string, bool> validator, string errorMessage = "Invalid input!")
        {
            while (true)
            {
                _output.Write(prompt);
                string input;

                if (prompt.Contains("Password"))
                {
                    StringBuilder password = new StringBuilder();
                    ConsoleKeyInfo key;

                    while(true)
                    {
                        key = Console.ReadKey(true);

                        if(key.Key == ConsoleKey.Enter)
                        {
                            _output.WriteLine("");
                            break;
                        }
                        else if(key.Key == ConsoleKey.Backspace && password.Length > 0)
                        {
                            password.Length--;
                            _output.Write("\b \b"); //remove asterisk
                        }
                        else if (!char.IsControl(key.KeyChar))
                        {
                            password.Append(key.KeyChar);
                            _output.Write("*"); //Add new Asterisk per char
                        }
                    }
                    input = password.ToString();

                }
                else
                {
                    input = _input.ReadLine();
                }

                if (validator(input))
                    return input.Trim();

                _system.DisplayMessage(errorMessage, true);
                _output.Clear(1000);
            }
        }
    }
}

namespace MenuFeatures
{
    using IOSystem;

    public class CampaignMode
    {
        private static readonly string[] story =
        {
            "Last Battle of Odyssey Campaign Mode Story\n",
            "Being born male granted you natural strength yet left you with less agility—had you been born female, the balance would have shifted, trading raw power for swifter grace.",
            "Beep, beep, beep…” You opened your eyes as you hear the mechanical sound of the heart machine next to you.",
            "As if trying to remind you the remaining limited heartbeats left in your life.",
            "Sadness slowly creeped inside.You tried moving like you always did and felt a sharp pain in your body.",
            "“Funny me.’ you whispered as you keep forgetting that your strength left your body ages ago and remembered the times you lived and moved full of life and vigor.",
            "Looking around the hospital room, you winced as the sunlight passed through the windows and bathed the whole room, dusts dancing as the new day started.",
            "You lived a good life, in this world’s standard that is.",
            "You were born in a fairly wealthy family that was able to meet everything you wanted and needed.",
            "Your parents were loving and supporting to whatever you did.",
            "The school was your kingdom—everyone wanted to be friends with you, teachers were fond of you, and your grades were above everyone.",
            "Come adulthood, many jobs offered opportunities and you did not waste them.",
            "Your pay made everyone jealous, a beautiful wife, lovely daughters and son.",
            "It was a good life, really, maybe even perfect.",
            "Most people would do anything just to live the fraction of your life.",
            "Memories flowed like an unstoppable wave of ocean, slowly reliving your life as you slowly close your eyes for the last time.",
            "You found yourself floating in a dark vast space filled with stars shining like a new promise of hope.",
            "A voice gently whispered, “Greetings Challenger, welcome to the Last Battle of Odyssey.”"
        };

        private static volatile bool skipTyping = false;
        private static volatile bool enterPressed = false;

        public void PrintCampaignStory(int typeSpeed = 20)
        {
            foreach (string line in story)
            {
                skipTyping = false;
                enterPressed = false;

                Thread listener = new Thread(() =>
                {
                    while (!enterPressed)
                    {
                        if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Enter)
                        {
                            skipTyping = true;
                            enterPressed = true;
                        }
                    }
                });
                listener.Start();

                foreach (char c in line)
                {
                    Console.Write(c);
                    if (!skipTyping)
                        Thread.Sleep(typeSpeed);
                }
                Console.WriteLine();

                if (!enterPressed)
                {
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                }
                listener.Join();
            }
            Console.WriteLine("\n--- End of Story ---");
            Console.WriteLine("\nPress ENTER to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }
    }

    public class Credits
    {
        public static readonly List<string> devs = new List<string>()
        {
            "ORPHIANO, NOEL RAFAEL J.",
            "CHOHAN, ABIDAAN",
            "SESE, BRYANN",
            "EXIT"
        };

        private readonly string[] NoelInfo =
        {
            "\nDEVELOPER: ORPHIANO, NOEL RAFAEL J.",
            "19(2025) years old, aspiring to be a good developer and programmer like his elder brothers",
            "He is a DEVOUT CATHOLIC, who likes to get along with people.",
            "Family oriented and the youngest among his siblings.",
            "CHRIST IS KING BTW - NOEL"
        };

        private readonly string[] AbiInfo =
        {
            "\nDEVELOPER: ABIDAAN, CHOHAN. 20 Years old (2025).",
            "Who loves watching anime and also playing games",
            "All while focusing on my dreams and achievement to stay resilient and focused in life."
        };

        private readonly string[] SeseInfo =
        {
            "\nDEVELOPER: SESE, BRYAN.",
            "Responsible for the documentation and developed the campaign story"
        };

        private void printDeveloperShortInfo(string[] info)
        {
            for (int i = 0; i < info.Length; i++)
            {
                foreach (char ch in info[i])
                {
                    Console.Write(ch);
                    Thread.Sleep(25);

                    if (info == NoelInfo && i == info.Length - 1)
                        Thread.Sleep(100);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nPress ENTER to continue...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }

        public void CreditsTo(string currentUser, MenuSystem _system, MenuNavigator _navigator)
        {
            bool InCredits = true;
            while (InCredits)
            {
                //Console.Clear();
                byte choice = _navigator.GetMenuChoice(
                    devs,
                    "\n====CREDITS-TO====\n\nDEVELOPERS: \n-CHOHAN, ABIDAAN, \n-SESE BRYANN, \n-ORPHIANO, NOEL RAFAEL J.",
                    "Hello " + currentUser + "!");

                switch (choice)
                {
                    case 1:
                        printDeveloperShortInfo(NoelInfo);
                        break;

                    case 2:
                        printDeveloperShortInfo(AbiInfo);
                        break;

                    case 3:
                        printDeveloperShortInfo(SeseInfo);
                        break;

                    case 4:
                        _system.DisplayMessage("Bye...", true);
                        Thread.Sleep(500);
                        return;
                }
            }
        }
    }
}

namespace Application
{
    using Information;
    using CharacterAndAccountDatabase;
    using IOSystem;
    using static CharacterAndAccountDatabase.LogSystem;
    using static Information.CharacterFeature;

    public class Confirmation
    {
        private readonly MenuSystem _system;
        private readonly OutputService _output;
        private readonly InputService _input;

        public Confirmation(MenuSystem _system, OutputService _output, InputService _input)
        {
            this._system = _system;
            this._output = _output;
            this._input = _input;
        }

        public enum ConfirmationResult
        {
            Yes, No, Cancelled
        }

        public ConfirmationResult AskAreYouSure(string title)
        {
            bool IsValidKey = false;
            while (true)
            {
                _output.Clear(0);
                _system.DisplayMessage($"Are you sure you want to go to {title}?\n");
                _output.WriteLine("1. Yes (requires Enter confirmation)");
                _output.WriteLine("2. Go Back To \'Log Menu\'");
                _output.Write("Input: ");

                string input = _input.ReadLine();

                if (byte.TryParse(input, out byte choice))
                {
                    if (choice == 1)
                    {
                        _system.DisplayMessage("\nPress [Enter] to confirm OR [ESC] to cancel");
                        while (!IsValidKey)
                        {
                            var key = Console.ReadKey(true);

                            if (key.Key == ConsoleKey.Enter)
                            {
                                IsValidKey = true;
                                return ConfirmationResult.Yes;
                            }
                            else if (key.Key == ConsoleKey.Escape)
                            {
                                IsValidKey = true;
                                _system.DisplayMessage("You cancelled...", true);
                                _output.Clear(1000);
                                return ConfirmationResult.Cancelled;
                            }
                            else
                            {
                                _system.DisplayMessage("Not a valid key, Please try again.", true);
                                _output.Clear(1000);
                                continue;
                            }
                        }
                    }
                    else if (choice == 2)
                    {
                        return ConfirmationResult.No;
                    }
                    else
                    {
                        _system.DisplayMessage("Please enter 1 or 2.", true);
                        _output.Clear(1000);
                    }
                }
                else
                {
                    _system.DisplayMessage("Invalid input. Please enter 1 or 2.", true);
                    _output.Clear(1000);
                }
            }
        }

        public ConfirmationResult AreYouSure(string ask, string title)
        {
            while (true)
            {
                _output.Clear(0);
                _system.DisplayMessage("Are you sure you wanted to " + ask + " account's " + title + "character?\n");
                _output.WriteLine("[1] YES, PLEASE  ");
                _output.WriteLine("[2] NO, GO BACK TO \'MAIN MENU\'");

                try
                {
                    byte input = Convert.ToByte(_input.ReadLine());
                    if (input == 1)
                        return ConfirmationResult.Yes;
                    else if (input == 2)
                        return ConfirmationResult.No;
                    else
                    {
                        continue;
                    }
                } catch(OverflowException e)
                {
                    _system.DisplayMessage("ERROR: " + e.Message, true);
                    _output.Clear(1000);
                } catch(FormatException e)
                {
                    _system.DisplayMessage("ERROR: " + e.Message, true);
                    _output.Clear(1000);
                }
            }
        }
    }

    public class UserHandler
    {
        private readonly MenuNavigator _navigator;
        private readonly MenuSystem _system;
        private readonly LogSystem _logSystem;
        private readonly OutputService _output;
        private readonly Confirmation _confirmation;

        public UserHandler(MenuNavigator _navigator, MenuSystem _system, LogSystem _logSystem, OutputService _output, Confirmation _confirmation)
        {
            this._navigator = _navigator;
            this._system = _system;
            this._logSystem = _logSystem;
            this._output = _output;
            this._confirmation = _confirmation;
        }

        public bool HandleAccountCreation(string title = "[--------Create-Account--------]\n")
        {
            var confirmation = _confirmation.AskAreYouSure("Create account");
            if (confirmation == Confirmation.ConfirmationResult.No || confirmation == Confirmation.ConfirmationResult.Cancelled)
                return false;

            if (confirmation == Confirmation.ConfirmationResult.Yes)
            {
                _output.Clear(0);
                _system.DisplayMessage(title);
                var username = _navigator.GetValidatedInput(
                    "Create Username: ",
                    isValidUsername,
                    "Username must be 7 or more character long!"
                    );
                var password = _navigator.GetValidatedInput(
                    "Create Password: ",
                    isValidPassword,
                    "Password must be 5 or more character long"
                    );

                AuthenticationResult result = _logSystem.CreateAccount(username, password);

                if (result == AuthenticationResult.InvalidCredentials)
                {
                    _system.DisplayMessage("Invalid Credentials. Please try again!", true);
                    _output.Clear(1000);
                    return false;
                }
                if (result == AuthenticationResult.UserExists)
                {
                    _system.DisplayMessage("Username already Exist. Please try again!", true);
                    _output.Clear(1000);
                    return false;
                }
                if (result == AuthenticationResult.Success)
                {
                    _system.DisplayMessage("Account Created!");
                    _output.Clear(1000);
                    return true;
                }
                else
                {
                    _system.DisplayMessage("Account Creation failed.", true);
                    _output.Clear(1000);
                    return false;
                }
            }
            return false;
        }

        public bool HandleLogin(out string username, string title = "[--------LOG-IN--------]\n")
        {
            var confirmation = _confirmation.AskAreYouSure("Log in");

            username = "";
            byte attempts = 3;

            while (attempts > 0 && confirmation == Confirmation.ConfirmationResult.Yes)
            {
                _output.Clear(0);
                _system.DisplayMessage(title);
                username = _navigator.GetValidatedInput("Username: ", isValidUsername, "Username too short. Please try again.");
                var password = _navigator.GetValidatedInput("Password: ", isValidPassword, "Password too short. Please try again.");

                AuthenticationResult result = _logSystem.LogIn(username, password, attempts);

                if (result == AuthenticationResult.Success)
                {
                    _system.DisplayMessage("Login successful!");
                    _output.Clear(1000);
                    return true;
                }
                _system.DisplayMessage("Invalid username or password!", true);

                attempts--;
                if (attempts > 0)
                    _system.DisplayMessage("You only have " + attempts + " attempts left remaining.");

                if (attempts == 0)
                    _system.DisplayMessage("You have no more attempts left. Please try again later", true);
            }

            if (confirmation == Confirmation.ConfirmationResult.No || confirmation == Confirmation.ConfirmationResult.Cancelled)
            {
                return false;
            }

            _output.Clear(1000);
            return false;
        }

        private static bool isValidUsername(string username) => !string.IsNullOrWhiteSpace(username) && username.Length >= 7;
        private static bool isValidPassword(string password) => !string.IsNullOrWhiteSpace(password) && password.Length >= 5;
    }

    public class CharacterCreationController
    {
        private readonly MenuNavigator _navigator;
        private readonly MenuSystem _system;
        private readonly CharacterSave _characterSaver;
        private readonly OutputService _output;
        private readonly InputService _input;
        private readonly Confirmation _confirmation;

        public CharacterCreationController(MenuNavigator navigator, MenuSystem system, CharacterSave characterSaver, OutputService output, InputService input, Confirmation confirmation)
        {
            _navigator = navigator;
            _system = system;
            _characterSaver = characterSaver;
            _output = output;
            _input = input;
            _confirmation = confirmation;
        }

        public Character CreateCharacter(string username)
        {
            CharacterBuilder builder = new CharacterBuilder();

            _system.DisplayMessage("Starting character creation...");
            _output.Clear(1000);

            builder.SetBasicInfo(
                SelectFromOption(Starters, "STARTER UNIVERSE"),
                SelectFromOption(Races, "RACE")
            );
            builder.SetGender(SelectFromOption(Genders, CharacterBuilder.GenderCondition()));
            builder.SetAppearance(
                SelectFromOption(Faces, "FACE:"),
                SelectFromOption(FaceShapes, "FACE SHAPE:"),
                SelectFromOption(FacialHairs, "FACIAL HAIR:"),
                SelectFromOption(HairStyles, "HAIR STYLE:")
            );
            builder.SetEyes(
                SelectFromOption(EyeShapes, "EYE SHAPE:"),
                SelectFromOption(EyeColors, "EYE COLOR:")
                );
            builder.SetFeatures(
                SelectFromOption(NoseShapes, "NOSE SHAPE:"),
                SelectFromOption(MouthOrLipShapes, "MOUTH/LIP SHAPE:"),
                SelectFromOption(EarShapes, "EAR SHAPE:"),
                SelectFromOption(BodyTypes, "BODY TYPE:")
                );
            builder.SetClothing(
                SelectFromOption(UpperClothings, "UPPER CLOTHING:"),
                SelectFromOption(PantsList, "PANTS:"),
                SelectFromOption(Shoes, "SHOES:"),
                SelectFromOption(Accessories, "ACCESSORIES:")
                );
            builder.SetAttributes(
                SelectFromOption(SkinTones, "SKIN TONE:"),
                SelectFromOption(Inventories, "INVENTORY:")
                );
            builder.SetSuperPower(SelectFromOption(SuperPowers, "SUPER POWER\n"));
            builder.SetBoss(SelectFromOption(FinalBosses, "FINAL BOSS\n"));

            Character character = builder.Build();
            builder.SetStats(character.Agility, character.Strength);

            bool characterSaved = false;
            do
            {
                _output.Clear(0);
                _output.Write("Set character title (must always be unique): ");
                character.Title = _input.ReadLine();

                if (_characterSaver.CharacterExists(username, character.Title))
                {
                    _system.DisplayMessage("Character title alr exists. Try again maybe", true);
                    _output.Clear(1000);
                    continue;
                }
                characterSaved = true;
            } while (!characterSaved);

            Console.WriteLine();

            //ToString() method from Character class
            _output.WriteLine(character.ToString()); //print all 

            _system.DisplayMessage("\nTitle: [" + character.Title + "] \n\n...Press [Enter] to Save...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

            _characterSaver.SaveCharacter(username, character);
            _system.DisplayMessage("\nCharacter created successfully!");
            _output.Clear(1000);
            return character;
        }

        private string SelectFromOption(string[] options, string optionName, string title = "[0] SKIP")
        {
            List<string> menuItems = new List<string>(options);
            int selection = _navigator.GetMenuChoice(menuItems, optionName, title);
            if (selection == 0) return "Unselected";
            return options[selection - 1];
        }

        //OVERLOAD, NEW METHOD 'SAME NAME'
        private T SelectFromOption<T>(List<T> option, string optionName, byte index = 0)
        {
            while (true)
            {
                _output.Clear();
                _output.Write("Select your " + optionName + "\n");
                for (var i = 0; i < option.Count; i++)
                {
                    Console.WriteLine((i + 1) + ". " + option[i]);
                }
                _output.Write("\nInput: ");

                try
                {
                    index = Convert.ToByte(Console.ReadLine());
                }
                catch (FormatException e)
                {
                    _system.DisplayMessage("Error: " + e.Message, true);
                    _output.Clear(1000);
                    continue;
                }
                if (index > option.Count || index < 1)
                {
                    _system.DisplayMessage("Invalid Input. Please try again.", true);
                    _output.Clear(1000);
                    continue;
                }
                break;
            }
            return option[index - 1];
        }

        public void askYRUinTheLoadGame(string username)
        {
            while (true)
            {
                _output.Clear(150);
                _system.DisplayMessage("Why are you here?\n");
                _output.WriteLine("[1] To delete characters");
                _output.WriteLine("[2] To load a game");
                _output.WriteLine("[3] Go back to \'Main Menu\'");

                _output.Write("\nInput: ");
                try
                {
                    byte choice = Convert.ToByte(Console.ReadLine());
                    if (choice == 1)
                    {
                        DeleteACharacter(username);
                        return;
                    }
                    else if (choice == 2)
                    {
                        LoadGame(username);
                        return;
                    }
                    else if(choice == 3)
                    {
                        return;
                    }
                    else
                    {
                        _system.DisplayMessage("Invalid number. Please try again", true);
                        _output.Clear(1000);
                        continue;
                    }
                }
                catch (FormatException e)
                {
                    _system.DisplayMessage("Invalid Format ERROR: [" + e.Message + "].", true);
                    _output.Clear(1000);
                    continue;
                }
                catch (OverflowException e)
                {
                    _system.DisplayMessage("Invalid ERROR: [" + e.Message + "].", true);
                    _output.Clear(1000);
                    continue;
                }
            }
        }

        private readonly DataBaseConnector dbMySql = new DataBaseConnector();

        public void DeleteACharacter(string username)
        {
            //This is where all the titles in title column will go so I can iterate through all titles using my helper method
            //(SAME TO LOAD GAME)
            List<string> titles = new List<string>();
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "SELECT title FROM characters WHERE username = @Username";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //dd all data from title column inside the database to the String List of titles
                //(SAME TO LOAD GAME)
                titles.Add(reader.GetString("title"));
            }

            if (titles.Count != 0)
            {
                int choice = _navigator.GetMenuChoice(titles, "\nLIST OF CHARACTERS: For \"" + username + "\" ", "[-----DELETE-CHARACTER-----]\n\n[0] EXIT");

                if (choice == 0)
                {
                    _system.DisplayMessage("EXITING...");
                    _output.Clear(1000);
                    return;
                }

                string chosenTitle = titles[choice - 1];

                var AreYouSure = _confirmation.AreYouSure("\'Delete\'", chosenTitle);

                if (AreYouSure == Confirmation.ConfirmationResult.No)
                {
                    _system.DisplayMessage("Cancelled...", true);
                    _output.Clear(1000);
                    return;
                }

                if (AreYouSure == Confirmation.ConfirmationResult.Yes)
                {
                    _characterSaver.DeleteCharacter(username, chosenTitle);
                    _output.Clear(2000);
                    return;
                }
            }

            _system.DisplayMessage("No created characters yet.", true);
            _output.Clear(1000);
        }

        public void LoadGame(string username)
        {
            List<string> titles = new List<string>();
            using var conn = dbMySql.GetConnection();
            conn.Open();

            string query = "SELECT title FROM characters WHERE username = @Username";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Username", username);

            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                titles.Add(reader.GetString("title"));
            }

            if(titles.Count != 0)
            {
                int choice = _navigator.GetMenuChoice(titles, "\nLIST OF SAVED CHARACTERS: For \"" + username + "\" ", "[-----LOAD-CHARACTER]-----]\n\n[0] EXIT");

                if(choice == 0)
                {
                    _system.DisplayMessage("EXITING...");
                    _output.Clear(1000);
                    return;
                }

                string chosenTitle = titles[choice - 1];

                var AreYouSure = _confirmation.AreYouSure("\'Load\'", chosenTitle);

                if (AreYouSure == Confirmation.ConfirmationResult.No)
                {
                    _system.DisplayMessage("Cancelled...", true);
                    _output.Clear(1000);
                    return;
                }

                if (AreYouSure == Confirmation.ConfirmationResult.Yes)
                {
                    LoadGameCharacter(username, chosenTitle);
                    return;
                }
            }

            _system.DisplayMessage("No created characters yet.", true);
            _output.Clear(1000);
        }

        private void LoadGameCharacter(string username, string title)
        {
            Character loadedCharacter = _characterSaver.LoadCharacter(username, title);
            _system.DisplayMessage("Character loaded successfully!");
            _output.Clear(250);

            //ToString() method from Character class
            _output.WriteLine(loadedCharacter.ToString()); //to print all

            _system.DisplayMessage("\nNOTIFICATION: [Character Title: " + title +" ] ... Press [Enter] ...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }
    }
}

namespace MainGameController
{
    using Application;
    using CharacterAndAccountDatabase;
    using Information;
    using IOSystem;
    using MenuFeatures;

    public class SuperHeroGame67
    {
        private readonly CharacterCreationController characterCreation;
        private readonly UserHandler userHandler;
        private readonly MenuNavigator menuNavigator;
        private readonly MenuSystem _system;
        private readonly CampaignMode campaign;
        private readonly Credits credits;

        public SuperHeroGame67(CharacterCreationController characterCreation, UserHandler userHandler, MenuNavigator menuNavigator, MenuSystem _system, CampaignMode campaign, Credits credits)
        {
            this.characterCreation = characterCreation;
            this.userHandler = userHandler;
            this.menuNavigator = menuNavigator;
            this._system = _system;
            this.campaign = campaign;
            this.credits = credits;
        }

        public void Run()
        {
            while (true)
            {
                var choice = menuNavigator.GetMenuChoice(MenuManager.logMenu, "LAST BATTLE IN ODYSSEY", "[--------SIGN-IN--------]\n");

                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        if (userHandler.HandleLogin(out string username))
                        {
                            new User(username, "");
                            MainMenu(username);
                        }
                        break;

                    case 2:
                        Console.Clear();
                        userHandler.HandleAccountCreation();
                        break;

                    case 3:
                        _system.DisplayMessage("PLAY \"EPIC BATTLE IN ODYSSEY!\" AGAIN NEXT TIME!");
                        Thread.Sleep(1000);
                        return;
                }
            }
        }

        public void MainMenu(string currentUser)
        {
            while (true)
            {
                var choice = menuNavigator.GetMenuChoice(MenuManager.mainMenu, "[LAST BATTLE IN ODYSSEY]", "Username: " + currentUser);

                switch (choice)
                {
                    case 1:
                        characterCreation.CreateCharacter(currentUser);
                        break;

                    case 2:
                        characterCreation.askYRUinTheLoadGame(currentUser);
                        break;

                    case 3:
                        campaign.PrintCampaignStory();
                        break;

                    case 4:
                        credits.CreditsTo(currentUser, _system, menuNavigator);
                        break;

                    case 5:
                        _system.DisplayMessage("Thank you for playing \"EPIC BATTLE IN ODYSSEY!\"");
                        Thread.Sleep(1000);
                        return;
                }
            }
        }
    }

    public class EpicBattleInOdyssey
    {
        public static void Main(string[] args)
        {
            InputService input = new InputService();
            OutputService output = new OutputService();
            MenuSystem _system = new MenuSystem(output);
            CharacterData characterData = new CharacterData(_system);
            CharacterSave characterSave = new CharacterSave(characterData);
            MenuNavigator menuNavigator = new MenuNavigator(input, output);
            Confirmation confirmation = new Confirmation(_system, output, input);
            CharacterCreationController characterCreation = new CharacterCreationController(menuNavigator, _system, characterSave, output, input, confirmation);
            AccountData accData = new AccountData();
            LogSystem logSystem = new LogSystem(accData);
            UserHandler userHandler = new UserHandler(menuNavigator, _system, logSystem, output,confirmation);
            CampaignMode campaign = new CampaignMode();
            Credits credits = new Credits();

            SuperHeroGame67 superHeroGame = new SuperHeroGame67(characterCreation, userHandler, menuNavigator, _system, campaign, credits);
            superHeroGame.Run();
        }
    }
}