import { provider } from './provider';

export interface plan {
  id: number;
  subtype: string;
  provider: provider;
  deductibles: number;
  maxCoverageAmount: Number;
  minIncomeEligibility: number;
  generalEligibility: string;
  commissionPercentage: string;
}
