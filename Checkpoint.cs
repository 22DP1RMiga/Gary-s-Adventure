// Cases: 6
// 1 case: before beating any boss
// 5 cases: after beating each boss
// {"0", "GTFO", "RAHG", "TRMX", "ASYR", "PHVM"};

class Checkpoint : User {
    private string[] checkpoints;
    private string filePath;
    
    public Checkpoint(string username, string[] checkpoints, string filePath) : base(username, checkpoints[0], filePath) {
        this.checkpoints = checkpoints;
        this.filePath = filePath;
    }
    
    public bool LookThroughCheckpoints(string input) {
        
        // This checks the valuable input for checkpoints
        foreach (string Cpoint in checkpoints) {
            if (input == Cpoint) {
                return true;
            } else if (input.Contains("0") || string.IsNullOrEmpty(input) || input.Contains(" ")) {
                input = "0";
                return true;
            }
        }
        return false;
    }
}