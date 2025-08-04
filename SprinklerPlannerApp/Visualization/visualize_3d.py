import os
import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

room_corners = [
    (97500.00, 34000.00, 2500.00),
    (85647.67, 43193.61, 2500.00),
    (91776.75, 51095.16, 2500.00),
    (103629.07, 41901.55, 2500.00)
]

pipe_segments = [
    [(98242.11, 36588.29, 3000.00), (87970.10, 44556.09, 3500.00)],
    [(99774.38, 38563.68, 3500.00), (89502.37, 46531.47, 3000.00)],
    [(101306.65, 40539.07, 3000.00), (91034.63, 48506.86, 3000.00)]
]

base_dir = os.path.dirname(__file__)
csv_path = os.path.join(base_dir, "..", "Resources", "output.csv")
df = pd.read_csv(csv_path)

fig = plt.figure(figsize=(14, 11))
ax = fig.add_subplot(111, projection='3d')
ax.set_title("3D Sprinkler Layout with Room and Pipes (with Coordinates)")

room_x, room_y, room_z = zip(*room_corners + [room_corners[0]])
ax.plot(room_x, room_y, room_z, color='black', label='Room Ceiling')

for pt in room_corners:
    ax.text(pt[0], pt[1], pt[2] + 200, f'({int(pt[0])}, {int(pt[1])}, {int(pt[2])})',
            fontsize=8, color='black')

for i, (start, end) in enumerate(pipe_segments):
    xs = [start[0], end[0]]
    ys = [start[1], end[1]]
    zs = [start[2], end[2]]
    ax.plot(xs, ys, zs, color='red', linewidth=2, label='Pipe' if i == 0 else None)

    ax.text(start[0], start[1], start[2] + 200, f'({int(start[0])},{int(start[1])},{int(start[2])})',
            fontsize=7, color='darkred')
    ax.text(end[0], end[1], end[2] + 200, f'({int(end[0])},{int(end[1])},{int(end[2])})',
            fontsize=7, color='darkred')

ax.scatter(df["SprinklerX"], df["SprinklerY"], df["SprinklerZ"], color='blue', label="Sprinklers")
ax.scatter(df["PipeX"], df["PipeY"], df["PipeZ"], color='green', marker='x', label="Pipe Connections")

for _, row in df.iterrows():
    ax.plot([row["SprinklerX"], row["PipeX"]],
            [row["SprinklerY"], row["PipeY"]],
            [row["SprinklerZ"], row["PipeZ"]],
            c='gray', alpha=0.5)

    ax.text(row["SprinklerX"], row["SprinklerY"], row["SprinklerZ"] + 200,
            f'({int(row["SprinklerX"])},{int(row["SprinklerY"])},{int(row["SprinklerZ"])})',
            fontsize=6, color='blue')

    ax.text(row["PipeX"], row["PipeY"], row["PipeZ"] + 200,
            f'({int(row["PipeX"])},{int(row["PipeY"])},{int(row["PipeZ"])})',
            fontsize=6, color='green')

ax.set_xlabel("X (mm)")
ax.set_ylabel("Y (mm)")
ax.set_zlabel("Z (mm)")
ax.view_init(elev=30, azim=45)
ax.legend(loc='upper right')
plt.tight_layout()

output_path = os.path.join(base_dir, "..", "Resources", "sprinkler_3d_with_coordinates.png")
plt.savefig(output_path, dpi=300)
print(f"Saved: {output_path}")
