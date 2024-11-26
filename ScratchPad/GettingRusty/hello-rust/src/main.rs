use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

fn main() {
    let file_lines: Vec<String> = parse_input();

    let instruction_spacing_position: Option<usize> =
        file_lines.iter().position(|s| s.trim().is_empty());

    if let Some(pos) = instruction_spacing_position {
        let instructions = &file_lines[..pos];
        let nodes = &file_lines[pos + 1..];

        println!("instructions: {:?}", instructions);
        println!("nodes: {:?}", nodes);
    } else {
        println!("No empty line found to split instructions and nodes.");
    }
}

fn parse_input() -> Vec<String> {
    // Hardcoded path to the text file
    let file_path = "PuzzleInput.txt";
    let mut file_lines: Vec<String> = Vec::new();

    // Check if the file exists
    if !Path::new(file_path).exists() {
        eprintln!("File '{}' does not exist.", file_path);
        return Vec::new();
    }

    // Open the file and read it line by line
    match File::open(file_path) {
        Ok(file) => {
            let reader = io::BufReader::new(file);

            // Print each line, preserving formatting
            for line_result in reader.lines() {
                match line_result {
                    Ok(line) => {
                        println!("{}", line);
                        file_lines.push(line);
                    } // Output the line
                    Err(err) => eprintln!("Error reading line: {}", err),
                }
            }
        }
        Err(err) => {
            eprintln!("Failed to open the file: {}", err);
        }
    }

    return file_lines;
}
