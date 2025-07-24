import os
import matplotlib.pyplot as plt
import matplotlib.patches as patches
import pandas as pd

def main():
    base_dir = os.path.dirname(__file__)
    resource_dir = os.path.join(base_dir, "..", "Resources")
    csv_path = os.path.join(resource_dir, "output.csv")
    output_image_path = os.path.join(resource_dir, "sprinkler_distribution.png")

    try:
        sprinkler_data = pd.read_csv(csv_path)
    except FileNotFoundError:
        print(f"File not found: {csv_path}")
        return

    room_polygon_coordinates = [
        (97500.00, 34000.00),
        (85647.67, 43193.61),
        (91776.75, 51095.16),
        (103629.07, 41901.55)
    ]

    room_outline_x = [pt[0] for pt in room_polygon_coordinates] + [room_polygon_coordinates[0][0]]
    room_outline_y = [pt[1] for pt in room_polygon_coordinates] + [room_polygon_coordinates[0][1]]

    pipe_segments = [
        [(98242.11, 36588.29), (87970.10, 44556.09)],
        [(99774.38, 38563.68), (89502.37, 46531.47)],
        [(101306.65, 40539.07), (91034.63, 48506.86)]
    ]

    fig, ax = plt.subplots(figsize=(12, 12))
    ax.set_title("Sprinkler Layout with Room Area and Pipes")

    room_patch = patches.Polygon(room_polygon_coordinates, closed=True, color='lightblue', alpha=0.2, label='Room Area')
    ax.add_patch(room_patch)

    ax.plot(room_outline_x, room_outline_y, color='black', linewidth=2)

    for idx, (start, end) in enumerate(pipe_segments):
        ax.plot([start[0], end[0]], [start[1], end[1]],
                color='red', linewidth=2, label='Water Pipe' if idx == 0 else None)

    ax.scatter(sprinkler_data['SprinklerX'], sprinkler_data['SprinklerY'],
               color='blue', label='Sprinklers', zorder=3)

    for _, row in sprinkler_data.iterrows():
        label = f"({int(row['SprinklerX'])},{int(row['SprinklerY'])})"
        ax.annotate(label, (row['SprinklerX'], row['SprinklerY']),
                    textcoords="offset points", xytext=(5, 5), fontsize=8, color='blue')

    ax.scatter(sprinkler_data['PipeX'], sprinkler_data['PipeY'],
               color='green', marker='x', label='Pipe Connections', zorder=2)

    for _, row in sprinkler_data.iterrows():
        ax.plot([row['SprinklerX'], row['PipeX']],
                [row['SprinklerY'], row['PipeY']],
                color='gray', linewidth=1, alpha=0.6)

    ax.set_xlabel("X (mm)")
    ax.set_ylabel("Y (mm)")
    ax.grid(True)
    ax.axis('equal')
    ax.legend(loc='upper right')
    plt.tight_layout()
    plt.savefig(output_image_path, dpi=300)
    print(f"Room-filled plot saved as '{output_image_path}'")

if __name__ == "__main__":
    main()
