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
    stage('Setup Output Dir') {
      steps {
        sh 'mkdir Plugin'
        sh 'mkdir Plugin/dependencies'
      }
    }
    stage('Package') {
      steps {
        sh 'mv bin/ToggleTag.dll Plugin/'
        sh 'mv bin/Newtonsoft.Json.dll Plugin/dependencies'
      }
    }
    stage('Archive') {
      steps {
		sh 'zip -r ToggleTag.zip Plugin/*'
		archiveArtifacts(artifacts: 'ToggleTag.zip', onlyIfSuccessful: true)
      }
    }
  }
}