pipeline {
    agent any 
    stages {
        stage('Build') { 
            steps {
                //  powershell 'mkdir artifacts'
                 powershell '.\\build.ps1 -Target CIBuild -Configuration "Production"'
            }
        }
    }
}