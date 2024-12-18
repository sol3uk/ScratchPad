﻿
#[cfg(test)]
pub mod aoc_2023_day_8_tests {
    use std::collections::HashMap;
    use std::fs::File;
    use std::io;
    use std::io::BufRead;
    use std::path::Path;

    #[derive(Debug)]
    struct NodeDirections {
        left: String,
        right: String,
    }

    #[test]
    fn day8_part1() {
        let file_lines: Vec<String> = parse_input();

        let instruction_spacing_position: Option<usize> =
            file_lines.iter().position(|s| s.trim().is_empty());
        let instructions: &String =
            &file_lines[..instruction_spacing_position.unwrap_or(file_lines.len())][0];
        let nodes: HashMap<String, NodeDirections> =
            construct_node_map(instruction_spacing_position, &file_lines);

        let mut current_node = "AAA".to_string();
        let mut number_of_steps = 0;

        while current_node != "ZZZ" {
            for instruction in instructions.chars() {
                number_of_steps += 1;
                if instruction == 'L' {
                    current_node = nodes.get(&current_node).unwrap().left.clone();
                } else if instruction == 'R' {
                    current_node = nodes.get(&current_node).unwrap().right.clone();
                }
            }
        }

        println!("Total number of steps: {:?}", number_of_steps);
        assert_eq!(number_of_steps, 13301);
    }

    #[test]
    fn day8_part2() {
        let file_lines: Vec<String> = parse_input();

        let instruction_spacing_position: Option<usize> =
            file_lines.iter().position(|s| s.trim().is_empty());
        let instructions: &String =
            &file_lines[..instruction_spacing_position.unwrap_or(file_lines.len())][0];
        let nodes: HashMap<String, NodeDirections> =
            construct_node_map(instruction_spacing_position, &file_lines);

        let current_nodes = nodes
            .iter()
            .filter(|(node, _)| node.ends_with("A"))
            .map(|(node, _)| node)
            .collect::<Vec<&String>>();
        let mut number_of_steps = 0;

        let mut node_maps: HashMap<String, i32> = HashMap::new();

        current_nodes.iter().for_each(|node| {
            let mut current_node = node.to_string();
            let starting_node = node.to_string();
            while !current_node.ends_with("Z") {
                for instruction in instructions.chars() {
                    number_of_steps += 1;
                    let left = &nodes.get(&current_node.to_string()).unwrap().left;
                    let right = &nodes.get(&current_node.to_string()).unwrap().right;

                    if instruction == 'L' {
                        current_node = left.to_string();
                    } else if instruction == 'R' {
                        current_node = right.to_string();
                    }
                }
            }
            node_maps.insert(starting_node.to_string(), number_of_steps);
            number_of_steps = 0;
        });

        let list_of_multiples = &node_maps.iter().map(|(_, &v)| v).collect::<Vec<i32>>();

        match lcm_of_array(list_of_multiples) {
            Some(multiple) => {
                println!("The lowest common multiple is: {}", multiple);
                assert_eq!(multiple, 7309459565207);
            }
            None => println!("No common multiple found."),
        }
    }

    /// Calculate the Greatest Common Divisor (GCD) using the Euclidean algorithm.
    fn gcd(a: i64, b: i64) -> i64 {
        let mut x = a.abs();
        let mut y = b.abs();
        while y != 0 {
            let temp = y;
            y = x % y;
            x = temp;
        }
        x
    }

    /// Calculate the Lowest Common Multiple (LCM) of two integers.
    fn lcm(a: i64, b: i64) -> i64 {
        a.abs() / gcd(a, b) * b.abs()
    }

    /// Calculate the LCM of an array of integers.
    fn lcm_of_array(numbers: &[i32]) -> Option<i64> {
        if numbers.is_empty() {
            return None;
        }

        numbers
            .iter()
            .map(|&n| n as i64)
            .reduce(lcm)
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
            nodes
        } else {
            println!("No empty line found to split instructions and nodes.");
            HashMap::new()
        }
    }

    fn parse_input() -> Vec<String> {
        let current_dir = Path::new(file!()).parent().unwrap();
        let file_name = "PuzzleInput.txt";
        let mut file_lines: Vec<String> = Vec::new();
        let file_path = current_dir.join(file_name);

        if !Path::new(&file_path).exists() {
            eprintln!("File '{}' does not exist.", file_name);
            return Vec::new();
        }

        match File::open(&file_path) {
            Ok(file) => {
                let reader: io::BufReader<File> = io::BufReader::new(file);

                for line_result in reader.lines() {
                    match line_result {
                        Ok(line) => {
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

        file_lines
    }
}