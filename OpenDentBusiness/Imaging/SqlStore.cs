using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Text;
using OpenDentBusiness;
using System.Data;
using System.IO;
using OpenDental.DataAccess;

namespace OpenDental.Imaging {
	public class SqlStore : ImageStoreBase {
		protected override Bitmap OpenImage(Document doc) {
			byte[] buffer = GetBytes(doc);
			MemoryStream stream = new MemoryStream(buffer);
			return new Bitmap(stream);
		}

		protected override byte[] GetBytes(Document doc) {
			byte[] buffer;
			using(IDbConnection connection = DataSettings.GetConnection())
			using(IDbCommand command = connection.CreateCommand()) {
				command.CommandText =
					@"SELECT Data FROM Files WHERE DocNum = ?DocNum";

				IDataParameter docNumParameter = command.CreateParameter();
				docNumParameter.ParameterName = "DocNum";
				docNumParameter.Value = doc.DocNum;
				command.Parameters.Add(docNumParameter);

				connection.Open();
				buffer = (byte[])command.ExecuteScalar();
				connection.Close();
			}
			return buffer;
		}

		protected override void SaveDocument(Document doc, Bitmap image) {
			SaveDocument(doc, image, image.RawFormat);
		}

		protected override void SaveDocument(Document doc, Bitmap image, ImageFormat format) {
			using(MemoryStream stream = new MemoryStream()) {
				image.Save(stream, format);
				SaveDocument(doc, stream);
			}
		}

		protected override void SaveDocument(Document doc, Bitmap image, ImageCodecInfo codec, EncoderParameters encoderParameters) {
			using(MemoryStream stream = new MemoryStream()) {
				image.Save(stream, codec, encoderParameters);
				SaveDocument(doc, stream);
			}
		}

		protected override void SaveDocument(Document doc, string filename) {
			using(FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read)) {
				SaveDocument(doc, stream);
			}
		}

		private void SaveDocument(Document doc, Stream stream) {
			// Copy the contents to a byte array
			int length = (int)stream.Length;
			byte[] buffer = new byte[length];
			stream.Read(buffer, 0, length);

			using(IDbConnection connection = DataSettings.GetConnection())
			using(IDbCommand command = connection.CreateCommand()) {
				command.CommandText =
					@"INSERT INTO Files (DocNum, Data, Thumbnail) VALUES (?DocNum, ?Data, NULL)";

				IDataParameter docNumParameter = command.CreateParameter();
				docNumParameter.ParameterName = "DocNum";
				docNumParameter.Value = doc.DocNum;
				command.Parameters.Add(docNumParameter);

				IDataParameter dataParameter = command.CreateParameter();
				dataParameter.ParameterName = "Data";
				dataParameter.Value = buffer;
				command.Parameters.Add(dataParameter);

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		protected override void DeleteThumbnailImageInternal(Document doc) {
			using(IDbConnection connection = DataSettings.GetConnection())
			using(IDbCommand command = connection.CreateCommand()) {
				command.CommandText =
					@"UPDATE Files SET Thumbnail = NULL WHERE DocNum = ?DocNum";

				IDataParameter docNumParameter = command.CreateParameter();
				docNumParameter.ParameterName = "DocNum";
				docNumParameter.Value = doc.DocNum;
				command.Parameters.Add(docNumParameter);

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}

		protected override void DeleteDocument(Document doc) {
			using(IDbConnection connection = DataSettings.GetConnection())
			using(IDbCommand command = connection.CreateCommand()) {
				command.CommandText =
					@"DELETE FROM files WHERE DocNum = ?DocNum";

				IDataParameter docNumParameter = command.CreateParameter();
				docNumParameter.ParameterName = "DocNum";
				docNumParameter.Value = doc.DocNum;
				command.Parameters.Add(docNumParameter);

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			}
		}
	}
}
