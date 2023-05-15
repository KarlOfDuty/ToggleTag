pipeline {
  agent any
  stages {
    stage('Dependencies') {
      steps {
        sh 'steamcmd +force_install_dir \$HOME/scpsl +login anonymous +app_update 996560 -beta public-beta validate +quit'
	    sh 'ln -s "\$HOME/scpsl/SCPSL_Data/Managed" ".scpsl_libs"'
        sh 'nuget restore ToggleTag.sln'
      }
    }
    stage('Build') {
      steps {
        sh 'msbuild ToggleTag.csproj -p:PostBuildEvent='
      }
    }
    stage('Package') {
      steps {
        sh 'mkdir dependencies'
        sh 'mv bin/ToggleTag.dll .'
        sh 'mv bin/Newtonsoft.Json.dll dependencies'
      }
    }
    stage('Archive') {
      steps {
		sh 'zip -r dependencies.zip dependencies'
		archiveArtifacts(artifacts: 'ToggleTag.dll', onlyIfSuccessful: true)
		archiveArtifacts(artifacts: 'dependencies.zip', onlyIfSuccessful: true)
      }
    }
  }
}