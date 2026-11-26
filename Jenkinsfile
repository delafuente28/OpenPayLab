pipeline
{
    agent
    {
        label 'dotnet-10'
    }
    options
    {
        skipDefaultCheckout(true)
    }
    stages
    {
        stage('Checkout')
        {
            steps
            {
                checkout scm
            }
        }
        stage('Clean Previous Test Results')
        {
            steps
            {
                sh 'rm -rf TestResults'
            }
        }
        stage('Restore')
        {
            steps
            {
                sh 'dotnet restore OpenPayLab.slnx'
                sh 'dotnet tool restore'
            }
        }
        stage('Build')
        {
            steps
            {
                sh 'dotnet build OpenPayLab.slnx --configuration Release --no-restore'
            }
        }
        stage('Tests')
        {
            steps
            {
                sh 'dotnet test OpenPayLab.slnx --configuration Release --no-build --no-restore --collect:"XPlat Code Coverage" --logger "trx;LogFileName=tests.trx" --results-directory TestResults'
            }
        }
    }
    post
    {
        always
        {
            script
            {
                if (fileExists('TestResults/tests.trx'))
                {
                    sh 'dotnet trx2junit TestResults/tests.trx'

                    if (fileExists('TestResults/tests.xml'))
                    {
                        junit 'TestResults/tests.xml'
                    }
                }
            }

            archiveArtifacts artifacts: 'TestResults/**/coverage.cobertura.xml',
                 allowEmptyArchive: false
        }
    }
}
