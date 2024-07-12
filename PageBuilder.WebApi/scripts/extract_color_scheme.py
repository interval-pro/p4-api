import sys
from sklearn.cluster import KMeans
import cv2
import numpy as np
from collections import Counter
import argparse


def rgb_to_hex(rgb):
    """Convert RGB color to HEX color."""
    return "#{:02x}{:02x}{:02x}".format(int(rgb[0]), int(rgb[1]), int(rgb[2]))


def remove_similar_colors(colors, threshold=30):
    """Remove similar colors from the list based on the given threshold."""
    filtered_colors = []
    for color in colors:
        if not filtered_colors:
            filtered_colors.append(color)
        else:
            if all(np.linalg.norm(np.array(color) - np.array(fc)) > threshold for fc in filtered_colors):
                filtered_colors.append(color)
    return filtered_colors


def extract_dominant_colors(image_path, num_colors=5, random_state=42):
    """Extract dominant colors from an image and return them in hexadecimal format."""
    try:
        # Load the image
        image = cv2.imread(image_path)
        if image is None:
            raise ValueError("Image not found or unable to read.")

        # Check if the image is black and white
        if len(image.shape) == 2 or image.shape[2] == 1:
            # The image is grayscale, read it in grayscale mode
            image = cv2.imread(image_path, cv2.IMREAD_GRAYSCALE)
            image = cv2.cvtColor(image, cv2.COLOR_GRAY2RGB)

        # Convert image from BGR to RGB format
        image = cv2.cvtColor(image, cv2.COLOR_BGR2RGB)

        # Reshape the image to be a list of pixels
        pixels = image.reshape(-1, 3)

        # Use KMeans clustering to find the dominant colors
        kmeans = KMeans(n_clusters=num_colors, random_state=random_state)
        kmeans.fit(pixels)

        # Get the cluster centers (dominant colors)
        dominant_colors = kmeans.cluster_centers_

        # Convert the colors to integer format
        dominant_colors = [list(map(int, color)) for color in dominant_colors]

        # Remove similar colors
        dominant_colors = remove_similar_colors(dominant_colors)

        # Convert the colors to hexadecimal format
        dominant_colors_hex = [rgb_to_hex(color) for color in dominant_colors]

        return dominant_colors_hex

    except Exception as e:
        print(f"Error: {e}")
        return []


def main():
    parser = argparse.ArgumentParser(description="Extract dominant colors from an image.")
    parser.add_argument("image_path", type=str, help="Path to the input image file.")
    parser.add_argument("--colors", type=int, default=5, help="Number of dominant colors to extract.")
    args = parser.parse_args()

    image_path = args.image_path
    num_colors = args.colors

    dominant_colors = extract_dominant_colors(image_path, num_colors)

    if dominant_colors:
        print("Dominant colors in the image:")
        for color in dominant_colors:
            print(color)
    else:
        print("No dominant colors found or unable to process the image.")


if __name__ == "__main__":
    main()
