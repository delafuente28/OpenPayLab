#!/usr/bin/env bash

set -eu

# Quality Gate thresholds
MIN_LINE_COVERAGE=80
MIN_BRANCH_COVERAGE=80

# Locate the Cobertura report
REPORT=$(find TestResults -type f \
    -name "coverage.cobertura.xml" \
    ! -path "*/In/*" \
    -print -quit)

if [[ -z "$REPORT" ]]; then
    echo "ERROR: Coverage report not found."
    exit 1
fi

# Extract global coverage rates
COVERAGE_LINE=$(grep -m1 '<coverage' "$REPORT" |
    sed -n 's/.*line-rate="\([0-9.]*\)".*/\1/p')

COVERAGE_BRANCH=$(grep -m1 '<coverage' "$REPORT" |
    sed -n 's/.*branch-rate="\([0-9.]*\)".*/\1/p')

if [[ -z "$COVERAGE_LINE" || -z "$COVERAGE_BRANCH" ]]; then
    echo "ERROR: Could not read coverage metrics."
    exit 1
fi

# Convert decimal ratios to percentages
LINE_PERCENT=$(awk -v value="$COVERAGE_LINE" \
    'BEGIN { printf "%.2f", value * 100 }')

BRANCH_PERCENT=$(awk -v value="$COVERAGE_BRANCH" \
    'BEGIN { printf "%.2f", value * 100 }')

echo "=== CODE COVERAGE ==="
echo "Line coverage:   ${LINE_PERCENT}%"
echo "Branch coverage: ${BRANCH_PERCENT}%"

echo "=== QUALITY GATE ==="

if ! awk -v actual="$LINE_PERCENT" \
    -v minimum="$MIN_LINE_COVERAGE" \
    'BEGIN { exit !(actual >= minimum) }'; then

    echo "FAILED: Line coverage is below ${MIN_LINE_COVERAGE}%."
    exit 1
fi

if ! awk -v actual="$BRANCH_PERCENT" \
    -v minimum="$MIN_BRANCH_COVERAGE" \
    'BEGIN { exit !(actual >= minimum) }'; then

    echo "FAILED: Branch coverage is below ${MIN_BRANCH_COVERAGE}%."
    exit 1
fi

echo "PASSED: All quality thresholds have been met."
