using Core.Seascape.BLL.Models;

namespace Core.Seascape.BLL.Services
{
    public interface ISeascapeService
    {
        /// <summary>
        /// Generates a random seascape image and returns the base64 data for the full size and lower half crop images.
        /// </summary>
        /// <param name="width">Specify an image width.</param>
        /// <param name="height">Specify an image height.</param>
        /// <returns>A full size and a lower half crop image in base64 string format.</returns>
        SeascapeImageData GenerateBase64Data(int width, int height);

        /// <summary>
        /// Generates a random seascape image and returns the base64 data for the full size and lower half crop images.
        /// </summary>
        /// <param name="model">A <see cref="SeascapeModel"/> containing image parameters.</param>
        /// <param name="retryOnError">If <see langword="true"/> a second pass to generate an image with default parameters is made if an error is caught during the first pass.</param>
        /// <returns>A full size and a lower half crop image in base64 string format.</returns>
        SeascapeImageData GenerateBase64Data(SeascapeModel model, bool retryOnError = true);
    }
}
