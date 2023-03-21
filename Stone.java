package stone;

import processing.core.*;
import java.util.ArrayList;
import java.util.concurrent.ThreadLocalRandom;

public class App extends PApplet {

    // Constants - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    int ROWS;
    int COLUMNS;
    int CELL_SIZE = 16;

    // Cells - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    // 0 -> Empty
    // 1 -> Obstacle
    // 2 -> Player
    // 3 -> Start
    // 4 -> End

    int step;
    int[][] currentMaze;
    int[][] nextMaze;

    boolean gameOver;
    boolean gameWin;
    boolean showNeighbors;
    boolean showIndexes;

    int playerRow;
    int playerColumn;
    boolean canMoveUp;
    boolean canMoveRight;
    boolean canMoveDown;
    boolean canMoveLeft;

    ArrayList<String> path;
    ArrayList<String> bestPath = new ArrayList<String>();;

    String[] lines;

    // Initial setup - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public void settings() {
        // size(800, 700);
        fullScreen();
    }

    public void setup() {
        step = 0;

        gameOver = false;
        gameWin = false;
        showNeighbors = false;
        showIndexes = false;

        playerRow = 0;
        playerColumn = 0;
        canMoveUp = false;
        canMoveRight = false;
        canMoveDown = false;
        canMoveLeft = false;

        path = new ArrayList<String>();

        lines = loadStrings("automatos/src/main/java/stone/maze_02.txt");
        for (int i = 0; i < lines.length; i++) {
            lines[i] = lines[i].replaceAll("\\s", "");
        }

        ROWS = lines.length;
        COLUMNS = lines[0].length();

        setupInitialState();

        nextGeneration();

        updatePlayerMoveOptions();
    }

    // Game loop - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public void draw() {
        background(100);
        fill(200);

        textSize((float) (CELL_SIZE * 0.40));
        textAlign(CENTER);

        // text("STEP: " + step, (float) (COLUMNS + 2.50) * CELL_SIZE, (float)
        // (CELL_SIZE * 0.50));
        text("BEST_PATH_SIZE: " + bestPath.size(), (float) (COLUMNS + 2.50) * CELL_SIZE, (float) (CELL_SIZE * 0.50));

        drawIndexes();

        translate(CELL_SIZE / 2, CELL_SIZE / 2);
        for (int row = 0; row < ROWS; row++) {
            for (int column = 0; column < COLUMNS; column++) {
                if (currentMaze[row][column] == 0) {
                    fill(200);
                }
                if (currentMaze[row][column] == 1) {
                    fill(0, 128, 0);
                }
                if (currentMaze[row][column] == 3 || currentMaze[row][column] == 4) {
                    fill(255, 255, 0);
                }

                rect(column * CELL_SIZE + CELL_SIZE / 2, row * CELL_SIZE + CELL_SIZE / 2,
                        CELL_SIZE, CELL_SIZE,
                        CELL_SIZE / 4);

                if (showNeighbors) {
                    int neighbors = getNeighbors(row, column, currentMaze);
                    fill(0);
                    text(neighbors, column * CELL_SIZE + CELL_SIZE, (float) (row * CELL_SIZE +
                            CELL_SIZE * 1.15));
                }
            }
        }

        fill(0);
        textAlign(LEFT);
        int directionCount = 0;
        for (String direction : path) {
            float x = directionCount * CELL_SIZE / 2;
            float y = (ROWS + 1) * CELL_SIZE + CELL_SIZE / 2;
            text(direction + " ", x, y);
            directionCount++;
        }

        fill(255, 0, 0);
        circle(playerColumn * CELL_SIZE + CELL_SIZE, playerRow * CELL_SIZE +
                CELL_SIZE, CELL_SIZE / 2);

        if (canMoveUp) {
            circle((float) (playerColumn * CELL_SIZE + CELL_SIZE),
                    (float) (playerRow * CELL_SIZE + CELL_SIZE * 0.50), CELL_SIZE / 4);

        }
        if (canMoveRight) {
            circle((float) (playerColumn * CELL_SIZE + CELL_SIZE * 1.50), playerRow *
                    CELL_SIZE + CELL_SIZE,
                    CELL_SIZE / 4);
        }
        if (canMoveDown) {
            circle((float) (playerColumn * CELL_SIZE + CELL_SIZE),
                    (float) (playerRow * CELL_SIZE + CELL_SIZE * 1.50), CELL_SIZE / 4);
        }
        if (canMoveLeft) {
            circle((float) (playerColumn * CELL_SIZE + CELL_SIZE * 0.50), playerRow *
                    CELL_SIZE + CELL_SIZE,
                    CELL_SIZE / 4);
        }

        keyCode = 10;
        keyPressed();
    }

    public void updatePlayerMoveOptions() {
        canMoveUp = (playerRow - 1) >= 0 && nextMaze[playerRow - 1][playerColumn] != 1;
        canMoveRight = (playerColumn + 1) < COLUMNS && nextMaze[playerRow][playerColumn + 1] != 1;
        canMoveDown = (playerRow + 1) < ROWS && nextMaze[playerRow + 1][playerColumn] != 1;
        canMoveLeft = (playerColumn - 1) >= 0 && nextMaze[playerRow][playerColumn - 1] != 1;
    }

    // Input handler - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    public void keyPressed() {
        if (gameWin) {
            return;
        }

        if (gameOver) {
            setup();

            gameOver = false;
            return;
        }

        if (keyCode == 73) {
            showIndexes = !showIndexes;
        }

        if (keyCode == 78) {
            showNeighbors = !showNeighbors;
        }

        if (keyCode == 10) {
            if (canMoveRight && canMoveDown) {
                int randomNum = ThreadLocalRandom.current().nextInt(0, 2);
                if (randomNum == 0) {
                    playerColumn++;
                    path.add("R");
                } else {
                    playerRow++;
                    path.add("D");
                }
            } else if (canMoveRight) {
                playerColumn++;
                path.add("R");
            } else if (canMoveDown) {
                playerRow++;
                path.add("D");
            } else if (canMoveLeft && canMoveUp) {
                int randomNum = ThreadLocalRandom.current().nextInt(0, 2);
                if (randomNum == 0) {
                    playerColumn--;
                    path.add("L");
                } else {
                    playerRow--;
                    path.add("U");
                }
            } else if (canMoveLeft) {
                playerColumn--;
                path.add("L");
            } else if (canMoveUp) {
                playerRow--;
                path.add("U");
            } else {
                gameOver = true;
                path.add("X");
            }

            if (currentMaze[playerRow][playerColumn] == 4) {
                path.add("V");
                if (bestPath.size() == 0) {
                    bestPath = path;
                }
                if (path.size() < bestPath.size()) {
                    bestPath = path;
                }
                gameOver = true;
            }

            step++;
            currentMaze = nextMaze;
            nextGeneration();

            updatePlayerMoveOptions();
        }
    }

    // Maze things - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
    void drawIndexes() {
        if (!showIndexes) {
            return;
        }

        for (int row = 0; row < ROWS; row++) {
            text(row, (float) (CELL_SIZE / 2), (float) (row * CELL_SIZE + CELL_SIZE * 1.70));
        }
        for (int column = 0; column < COLUMNS; column++) {
            text(column, (float) (column * CELL_SIZE + CELL_SIZE * 1.50), (float) (CELL_SIZE * 0.70));
        }
    }

    void fillRow(int row, int[] columns) {
        for (int column : columns) {
            currentMaze[row][column] = 1;
        }
    }

    void setupInitialState() {
        currentMaze = new int[ROWS][COLUMNS];

        for (int row = 0; row < ROWS; row++) {
            for (int column = 0; column < COLUMNS; column++) {
                currentMaze[row][column] = Integer.parseInt(String.valueOf(lines[row].charAt(column)));
            }
        }
    }

    int getNeighbors(int row, int column, int[][] maze) {
        int neighbors = 0;

        if ((row - 1) >= 0 && (column - 1) >= 0) {
            if (maze[row - 1][column - 1] == 1) {
                neighbors++;
            }
        }
        if ((row - 1) >= 0) {
            if (maze[row - 1][column] == 1) {
                neighbors++;
            }
        }
        if ((row - 1) >= 0 && (column + 1) < COLUMNS) {
            if (maze[row - 1][column + 1] == 1) {
                neighbors++;
            }
        }

        if ((column - 1) >= 0) {
            if (maze[row][column - 1] == 1) {
                neighbors++;
            }
        }
        if ((column + 1) < COLUMNS) {
            if (maze[row][column + 1] == 1) {
                neighbors++;
            }
        }

        if ((row + 1) < ROWS && (column - 1) >= 0) {
            if (maze[row + 1][column - 1] == 1) {
                neighbors++;
            }
        }
        if ((row + 1) < ROWS) {
            if (maze[row + 1][column] == 1) {
                neighbors++;
            }
        }
        if ((row + 1) < ROWS && (column + 1) < COLUMNS) {
            if (maze[row + 1][column + 1] == 1) {
                neighbors++;
            }
        }

        return neighbors;
    }

    void nextGeneration() {
        nextMaze = new int[ROWS][COLUMNS];

        for (int row = 0; row < ROWS; row++) {
            for (int column = 0; column < COLUMNS; column++) {
                int value = Integer.parseInt(String.valueOf(lines[row].charAt(column)));
                nextMaze[row][column] = (value != 3 && value != 4) ? 0 : value;
            }
        }

        for (int row = 0; row < ROWS; row++) {
            for (int column = 0; column < COLUMNS; column++) {
                int neighbors = getNeighbors(row, column, currentMaze);

                if (currentMaze[row][column] == 0) {
                    if (neighbors == 2 || neighbors == 3) {
                        nextMaze[row][column] = 1;
                    }
                } else if (currentMaze[row][column] == 1) {
                    if (neighbors == 4 || neighbors == 5 || neighbors == 6) {
                        nextMaze[row][column] = 1;
                    }
                }
            }
        }
    }

    static public void main(String[] passedArgs) {
        String[] appletArgs = new String[] { "stone.App" };

        PApplet.main(appletArgs);
    }
}
