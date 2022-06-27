pipeline {
    agent any 
    stages {
        stage('Build') { 
            steps {
                 powershell 'ni artifacts'
                 powershell '.\\build.ps1 -Target CIBuild -Configuration "Production"'
            }
        }
    }
}