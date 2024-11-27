use std::collections::HashMap;
use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

#[derive(Debug, Eq, PartialEq, Hash)]
struct Node {
    identifier: String,
}

#[derive(Debug)]
struct NodeDirections {
    left: String,
    right: String,
}

fn main() {
    let file_lines: Vec<String> = parse_input();

    let instruction_spacing_position: Option<usize> =
        file_lines.iter().position(|s| s.trim().is_empty());

    let instructions: &[String] =
        &file_lines[..instruction_spacing_position.unwrap_or(file_lines.len())].;
    let nodes: HashMap<Node, NodeDirections> =
        construct_node_map(instruction_spacing_position, &file_lines);
    println!("instructions: {:?}", instructions);
    nodes.iter().for_each(|(node, directions)| {
        println!("Node: {:?}, Directions: {:?}", node, directions);
    });
}

fn construct_node_map(
    instruction_spacing_position: Option<usize>,
    file_lines: &[String],
) -> HashMap<Node, NodeDirections> {
    if let Some(pos) = instruction_spacing_position {
        let raw_nodes: &[String] = &file_lines[pos + 1..];
        let nodes: HashMap<Node, NodeDirections> = raw_nodes
            .iter()
            .map(|line: &String| {
                let parts: Vec<&str> = line.split(" = ").collect();
                let node = Node {
                    identifier: parts[0].to_string(),
                };
                let cleaned_parts = parts[1].replace("(", "").replace(")", "");
                let raw_directions: Vec<&str> = cleaned_parts.split(", ").collect();
                let directions = NodeDirections {
                    left: raw_directions[0].to_string(),
                    right: raw_directions[1].to_string(),
                };
                (node, directions)
            })
            .collect();
        return nodes;
    } else {
        println!("No empty line found to split instructions and nodes.");
        return HashMap::new();
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
            let reader: io::BufReader<File> = io::BufReader::new(file);

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
