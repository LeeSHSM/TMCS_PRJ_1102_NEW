using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace LshCamera
{
    public class CameraDBManager
    {
        private string _connectionString;

        public void SetDBConectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        private async Task<bool> IsDatabaseConnected()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    return connection.State == ConnectionState.Open;
                }
                catch (SqlException e)
                {
                    // 연결 시도 중 예외가 발생했습니다.
                    Console.WriteLine("Database connection failed: " + e.Message);
                    return false;
                }
            }
        }

        public async Task SavePresetAsync(ICamera camera,CameraPreset preset)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                // 먼저 해당 presetid와 cameraid 조합이 데이터베이스에 존재하는지 확인
                string checkQuery = "SELECT COUNT(1) FROM tbl_camera_preset WHERE cameraid = @cameraId AND presetid = @presetId";
                using (SqlCommand checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue("@cameraId", camera.CameraId);
                    checkCommand.Parameters.AddWithValue("@presetId", preset.Presetid);

                    int count = (int)await checkCommand.ExecuteScalarAsync();
                    string query;

                    if (count > 0)
                    {
                        // Update existing preset
                        query = "UPDATE tbl_camera_preset SET presetname = @presetName, presetposition = @presetPosition WHERE cameraid = @cameraId AND presetid = @presetId";
                    }
                    else
                    {
                        // Insert new preset
                        query = "INSERT INTO tbl_camera_preset (cameraid, presetid, presetname, presetposition) VALUES (@cameraId, @presetId, @presetName, @presetPosition)";
                    }

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cameraId", camera.CameraId);
                        command.Parameters.AddWithValue("@presetId", preset.Presetid);
                        command.Parameters.AddWithValue("@presetName", preset.Presetname);
                        command.Parameters.AddWithValue("@presetPosition", ConvertPresetPositionToString(preset.Presetposition));

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
        }

        private string ConvertPresetPositionToString(byte[] positionBytes)
        {
            return string.Join(",", positionBytes);
        }

        public async Task SavePresetGroupAsync(ICamera camera, CameraPresetGroup presetGroup)
        {

        }

        public async Task<CameraPresetGroup> GetPresetAsync(ICamera camera)
        {
            CameraPresetGroup presetGroup = new CameraPresetGroup { CameraId = camera.CameraId, Presets = new List<CameraPreset>() };
            //if (await IsDatabaseConnected())
            //{
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                    await connection.OpenAsync();

                    string query = "SELECT presetid, presetname, presetposition FROM tbl_camera_preset WHERE cameraid = @cameraId";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@cameraId", camera.CameraId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                string presetPosition = reader.IsDBNull(reader.GetOrdinal("presetposition")) ? string.Empty : reader.GetString(reader.GetOrdinal("presetposition"));
                                CameraPreset preset = new CameraPreset
                                {
                                    Presetid = reader.GetInt32(reader.GetOrdinal("presetid")),
                                    Presetname = reader.GetString(reader.GetOrdinal("presetname")),
                                    Presetposition = ParsePresetPosition(presetPosition)
                                };

                                presetGroup.Presets.Add(preset);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            //}
            return presetGroup;
        }


        private byte[] ParsePresetPosition(string positionString)
        {
            if (string.IsNullOrEmpty(positionString))
                return new byte[0];

            string[] positionParts = positionString.Split(',');
            byte[] positionBytes = new byte[positionParts.Length];
            for (int i = 0; i < positionParts.Length; i++)
            {
                if (byte.TryParse(positionParts[i].Trim(), out byte parsedByte))
                {
                    positionBytes[i] = parsedByte;
                }
                else
                {
                    // 오류 처리: 변환 실패
                    throw new FormatException("Invalid format in Preset Position string.");
                }
            }

            return positionBytes;
        }



    }
}
