using System;

using R5T.T0132;


namespace R5T.S0043
{
	[DraftFunctionalityMarker]
	public partial interface IFilePathOperator : IDraftFunctionalityMarker
	{
		public string GetBackupCopyFileNameStem(string fileNameStem)
		{
			var output = $"{fileNameStem}-BAK";
			return output;
		}

		public string GetBackupCopyFileName(string fileName)
        {
			var fileNameStem = Instances.FileNameOperator_Base.GetFileNameStem(fileName);
			var fileExtension = Instances.FileNameOperator_Base.GetFileExtension(fileName);

			var backupCopyFileNameStem = this.GetBackupCopyFileNameStem(fileNameStem);

			var backupCopyFileName = Instances.FileNameOperator_Base.GetFileName(backupCopyFileNameStem, fileExtension);
			return backupCopyFileName;
		}

		public string GetBackupCopyFilePath(string filePath)
        {
			var parentDirectoryPath = Instances.PathOperator.GetParentDirectoryPath_ForFile(filePath);
			var fileName = Instances.PathOperator.GetFileName(filePath);

			var backupCopyFileName = this.GetBackupCopyFileName(fileName);

			var backupCopyFilePath = Instances.PathOperator.GetFilePath(
				parentDirectoryPath,
				backupCopyFileName);

			return backupCopyFilePath;
        }
	}
}