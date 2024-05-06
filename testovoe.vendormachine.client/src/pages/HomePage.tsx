import CoinlistComponent from "../components/CoinListComponent";
import InfoBarComponent from "../components/InfoBarComponent";
import SodaListComponent from "../components/SodaListComponent";

export default function HomePage() {
  return (
    <div className="flex flex-col items-center">
      <span className="text-neon mt-3 select-none text-6xl italic sm:text-7xl">
        SeregaSoda
      </span>
      <SodaListComponent />
      <InfoBarComponent />
      <CoinlistComponent />
    </div>
  );
}
