pipeline {
    agent any 
    stages {
        stage('Build') { 
            steps {
                 powershell '.\\build.ps1 -Target CIBuild -Configuration "Production"'
                //  powershell -noexit "& "".\\run-tests.ps1"""
            }
        }
    }
}