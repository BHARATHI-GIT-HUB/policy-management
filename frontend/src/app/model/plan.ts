import { Provider } from './provider';

export interface plan {
  id: number;
  subtype: string;
  type: string;
  provider: Provider;
  deductibles: number;
  maxCoverageAmount: number;
  minIncomeEligibility: number;
  generalEligibility: string;
  commissionPercentage: string;
}
