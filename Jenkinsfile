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
        stage('Restore')
        {
            steps
            {
                sh 'dotnet restore OpenPayLab.slnx'
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
                sh 'dotnet test OpenPayLab.slnx --configuration Release --no-build --no-restore'
            }
        }
    }
}
