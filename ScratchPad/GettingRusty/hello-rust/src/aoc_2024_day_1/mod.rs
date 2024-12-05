
#[cfg(test)]
pub mod aoc_2024_day_1_tests {
    use std::fs::File;
    use std::io;
    use std::io::BufRead;
    use std::path::Path;

    #[test]
    fn part1() {
        let file_lines: Vec<String> = parse_input();
        
    }

    #[test]
    fn part2() {

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