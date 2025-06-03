import numpy as np
import pandas as pd
import scipy.stats as stats
import matplotlib.pyplot as pyplot
from scipy.stats import fisher_exact

# Each algorithm was tested 200 times
# Format: [passes, fails]
results = {
    "PathFirst": [200, 0],     
    "CorridorConsidered": [200, 0],
    "Random": [1, 199]
}

# Create a contingency table for the chi-square test
observed = np.array([
    results["PathFirst"],
    results["CorridorConsidered"],
    results["Random"]
])

# Run Chi-square test
chi2, p, dof, expected = stats.chi2_contingency(observed)

# Print results
print("Chi-Square test results:")
print(f"Chi2 Statistic: {chi2:.4f}")
print(f"P-value: {p:.4f}")

pathFirstVsRandom = np.array([
    results["PathFirst"],
    results["Random"]
])
oddsratio, p_value = fisher_exact(pathFirstVsRandom)
print("PathFirst vs Random p value:", p_value)

corridorConsideredtVsRandom = np.array([
    results["CorridorConsidered"],
    results["Random"]
])
oddsratio, p_value = fisher_exact(corridorConsideredtVsRandom)
print("CorridorConsidered vs Random p value:", p_value)

corridorConsideredVsPathFirst = np.array([
    results["CorridorConsidered"],
    results["PathFirst"]
])
oddsratio, p_value = fisher_exact(corridorConsideredVsPathFirst)
print("CorridorConsidered vs PathFirst p value:", p_value)


#Generating bar chart of results
algorithmsToPlot = ["Path First", "Corridor Considered", "Random"]
resultsToPlot = [200, 200, 1]

pyplot.bar(algorithmsToPlot, resultsToPlot, color="skyblue")
pyplot.xlabel("Algorithms")
pyplot.ylabel("Number of successful generations")
pyplot.title("Number of times each asset placement algorithm generated successful generations")

for i, value in enumerate(resultsToPlot):
    pyplot.text(i, value + 5, str(value), ha='center')

pyplot.show()