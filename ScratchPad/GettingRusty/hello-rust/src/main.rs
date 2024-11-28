use std::collections::HashMap;
use std::fs::File;
use std::io::{self, BufRead};
use std::path::Path;

#[derive(Debug)]
struct NodeDirections {
    left: String,
    right: String,
}

fn main() {
    // day8_part1();
    day8_part2();
}

fn day8_part1() {
    let file_lines: Vec<String> = parse_input();

    let instruction_spacing_position: Option<usize> =
        file_lines.iter().position(|s| s.trim().is_empty());
    let instructions: &String =
        &file_lines[..instruction_spacing_position.unwrap_or(file_lines.len())][0];
    let nodes: HashMap<String, NodeDirections> =
        construct_node_map(instruction_spacing_position, &file_lines);

    // nodes.iter().for_each(|(node, directions)| {
    //     println!("Node: {:?}, Directions: {:?}", node, directions);
    // });

    let mut current_node = "AAA".to_string();
    let mut number_of_steps = 0;

    while current_node != "ZZZ" {
        for instruction in instructions.chars() {
            number_of_steps += 1;
            if instruction == 'L' {
                println!(
                    "Current Node: {:?} Instruction: Turn left, Number of steps: {:?}",
                    current_node, number_of_steps
                );
                current_node = nodes.get(&current_node).unwrap().left.clone();
            } else if instruction == 'R' {
                println!(
                    "Current Node: {:?} Instruction: Turn right, Number of steps: {:?}",
                    current_node, number_of_steps
                );
                current_node = nodes.get(&current_node).unwrap().right.clone();
            }
        }
    }
}

fn day8_part2() {
    let file_lines: Vec<String> = parse_input();

    let instruction_spacing_position: Option<usize> =
        file_lines.iter().position(|s| s.trim().is_empty());
    let instructions: &String =
        &file_lines[..instruction_spacing_position.unwrap_or(file_lines.len())][0];
    let nodes: HashMap<String, NodeDirections> =
        construct_node_map(instruction_spacing_position, &file_lines);

    // nodes.iter().for_each(|(node, directions)| {
    //     println!("Node: {:?}, Directions: {:?}", node, directions);
    // });

    let mut current_nodes = nodes.iter()
        .filter(|(node, _)| node.ends_with("A"))
        .map(|(node, _)| node)
        .collect::<Vec<&String>>();
    let mut number_of_steps = 0;

    while !current_nodes.iter().all(|node| node.ends_with("Z")) {
        for instruction in instructions.chars() {
            number_of_steps += 1;


            let new_nodes = current_nodes.iter().fold(Vec::new(), |mut acc, node| {
                let left = &nodes.get(&node.to_string()).unwrap().left;
                let right = &nodes.get(&node.to_string()).unwrap().right;
                if instruction == 'L' {
                    acc.push(left);
                } else if instruction == 'R' {
                    acc.push(right);
                }
                acc
            });
            println!(
                "Current Nodes: {:?} Number of steps: {:?}",
                new_nodes, number_of_steps
            );
            current_nodes = new_nodes;
        }
    }
}

fn construct_node_map(
    instruction_spacing_position: Option<usize>,
    file_lines: &[String],
) -> HashMap<String, NodeDirections> {
    if let Some(pos) = instruction_spacing_position {
        let raw_nodes: &[String] = &file_lines[pos + 1..];
        let nodes: HashMap<String, NodeDirections> = raw_nodes
            .iter()
            .map(|line: &String| {
                let parts: Vec<&str> = line.split(" = ").collect();
                let node = parts[0].to_string();
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
    let file_path = "PuzzleInput.txt";
    let mut file_lines: Vec<String> = Vec::new();

    if !Path::new(file_path).exists() {
        eprintln!("File '{}' does not exist.", file_path);
        return Vec::new();
    }

    match File::open(file_path) {
        Ok(file) => {
            let reader: io::BufReader<File> = io::BufReader::new(file);

            for line_result in reader.lines() {
                match line_result {
                    Ok(line) => {
                        // println!("{}", line);
                        file_lines.push(line);
                    }
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
