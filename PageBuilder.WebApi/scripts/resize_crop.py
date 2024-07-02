import argparse
from PIL import Image
import os


def resize_and_crop_image(image_path, target_width, target_height, quality):
    try:
        img = Image.open(image_path)
    except IOError as e:
        print(f"Error: Unable to open image file '{image_path}'. {e}")
        return

    # Convert the image to 'RGB' mode if it has an alpha channel
    if img.mode == 'RGBA':
        img = img.convert('RGB')

    # Calculate the new size while maintaining the aspect ratio
    original_width, original_height = img.size
    aspect_ratio = original_width / original_height
    target_aspect_ratio = target_width / target_height

    # Resize the image
    if aspect_ratio > target_aspect_ratio:
        # Image is wider than the target aspect ratio
        new_height = target_height
        new_width = int(new_height * aspect_ratio)
    else:
        # Image is taller than the target aspect ratio
        new_width = target_width
        new_height = int(new_width / aspect_ratio)

    resized_img = img.resize((new_width, new_height), Image.LANCZOS)

    # Center crop the image to the target size
    left = (new_width - target_width) / 2
    top = (new_height - target_height) / 2
    right = (new_width + target_width) / 2
    bottom = (new_height + target_height) / 2
    cropped_img = resized_img.crop((left, top, right, bottom))

    # Construct the output filename
    output_filename = os.path.splitext(os.path.basename(image_path))[0] + f"_resized_{target_width}x{target_height}x{quality}.png"
    output_path = os.path.join(os.path.dirname(image_path), output_filename)

    # Save the resized and cropped image with optimization
    try:
        if quality.lower() == 'high':
            cropped_img.save(output_path, format='PNG', optimize=True)
        elif quality.lower() == 'low':
            cropped_img.save(output_path, format='PNG', optimize=False)
        else:
            print("Error: Quality must be either 'low' or 'high'")
            return
        print(f"Resized and cropped image saved as '{output_path}' with quality '{quality}'")
    except IOError as e:
        print(f"Error: Unable to save the resized and cropped image to '{output_path}'. {e}")


def validate_args(width, height, quality):
    if width is not None and width <= 0:
        raise ValueError("Width must be greater than zero.")
    if height is not None and height <= 0:
        raise ValueError("Height must be greater than zero.")
    if quality not in ['low', 'high']:
        raise ValueError("Quality must be 'low' or 'high'.")


def main():
    # Create an ArgumentParser object to handle command-line arguments
    parser = argparse.ArgumentParser(description='Resize and crop an image to specified dimensions while maintaining aspect ratio.')
    parser.add_argument('imagePath', type=str, help='Path to the input image')
    parser.add_argument('width', type=int, nargs='?', default=None, help='Desired width of the output image')
    parser.add_argument('height', type=int, nargs='?', default=None, help='Desired height of the output image')
    parser.add_argument('quality', type=str, choices=['low', 'high'], help='Quality of the output image (low or high)')

    args = parser.parse_args()

    try:
        validate_args(args.width, args.height, args.quality)
    except ValueError as e:
        print(e)
        return

    if not args.width and not args.height:
        print("At least one of width or height must be specified!")
        return

    resize_and_crop_image(args.imagePath, args.width, args.height, args.quality)


if __name__ == '__main__':
    main()
