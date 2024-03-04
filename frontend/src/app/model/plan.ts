import { Provider } from './provider';

export interface plan {
  id: number;
  subtype: string;
  provider: Provider;
  deductibles: number;
  maxCoverageAmount: Number;
  minIncomeEligibility: number;
  generalEligibility: string;
  commissionPercentage: string;
}
