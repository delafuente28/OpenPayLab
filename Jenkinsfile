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
        stage('SonarQube Begin Analysis')
        {
            steps
            {
                script
                {
                    env.SONAR_SCANNER_HOME = tool 'SonarScannerForDotNet'
                }

                withSonarQubeEnv('SonarQubeCloud')
                {
                    sh '''
                        dotnet "$SONAR_SCANNER_HOME/SonarScanner.MSBuild.dll" begin \
                            /k:"delafuente28_OpenPayLab" \
                            /o:"delafuente28" \
                            /d:sonar.token="$SONAR_AUTH_TOKEN" \
                            /d:sonar.cs.opencover.reportsPaths="TestResults/**/coverage.opencover.xml"
                    '''
                }
            }
        }
        stage('Build')
        {
            steps
            {
                sh 'dotnet build OpenPayLab.slnx --configuration Release --no-restore --no-incremental --disable-build-servers'
            }
        }
        stage('Tests')
        {
            steps
            {
                sh 'dotnet test OpenPayLab.slnx --configuration Release --no-build --no-restore --collect:"XPlat Code Coverage" --logger "trx;LogFileName=tests.trx" --results-directory TestResults -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format=opencover,cobertura'
            }
        }
        stage('SonarQube End Analysis')
        {
            steps
            {
                withSonarQubeEnv('SonarQubeCloud')
                {
                    sh '''
                        dotnet "$SONAR_SCANNER_HOME/SonarScanner.MSBuild.dll" end \
                            /d:sonar.token="$SONAR_AUTH_TOKEN"
                    '''
                }
            }
        }
        stage('Quality Gate')
        {
            steps
            {
                sh 'bash scripts/check-coverage.sh'
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
