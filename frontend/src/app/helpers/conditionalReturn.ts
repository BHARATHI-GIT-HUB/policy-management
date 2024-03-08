export function agePercentage(age: number): number {
  let premiumFactor: number = 0.0;

  if (age > 60) {
    premiumFactor = 0.5;
  } else if (age > 50) {
    premiumFactor = 0.6;
  } else if (age > 40) {
    premiumFactor = 0.7;
  } else if (age > 30) {
    premiumFactor = 0.8;
  } else if (age > 25) {
    premiumFactor = 0.9;
  } else if (age > 20) {
    premiumFactor = 1;
  } else {
    return premiumFactor;
  }

  return premiumFactor;
}

export function timePeriod(timePeriod: string): number {
  switch (timePeriod) {
    case 'Yearly':
      return 1;
    case 'Half Yearly':
      return 2;
    case 'Quarterly':
      return 4;
    case 'Monthly':
      return 12;
    default:
      return 0;
  }
}

export function totalTimePeriod(age: number): number {
  let timePeriod: number = 0;

  if (age > 60) {
    timePeriod = 2;
  } else if (age > 50) {
    timePeriod = 2;
  } else if (age > 40) {
    timePeriod = 2;
  } else if (age > 30) {
    timePeriod = 3;
  } else if (age > 25) {
    timePeriod = 4;
  } else if (age > 20) {
    timePeriod = 5;
  } else {
    return timePeriod;
  }

  return timePeriod;
}
