{
  description = "Flake providing a package for Final Frontier Launcher.";

  inputs.nixpkgs.url = "github:NixOS/nixpkgs/nixpkgs-unstable";

  outputs =
    { self, nixpkgs, ... }:
    let
      forAllSystems =
        function:
        nixpkgs.lib.genAttrs [ "x86_64-linux" ] # TODO: aarch64-linux support
          (system: function system (import nixpkgs { inherit system; }));
    in
    {

      packages = forAllSystems (
        system: pkgs: {
          default = self.packages.${system}.final-frontier-launcher;
          final-frontier-launcher = pkgs.callPackage ./nix/package.nix { };
        }
      );

      overlays = {
        default = self.overlays.final-frontier-launcher;
        final-frontier-launcher = final: prev: {
          final-frontier-launcher =
            self.packages.${prev.stdenv.hostPlatform.system}.final-frontier-launcher;
        };
      };

      apps = forAllSystems (
        system: pkgs:
        let
          pkg = self.packages.${system}.final-frontier-launcher;
        in
        {
          default = self.apps.${system}.final-frontier-launcher;
          final-frontier-launcher = {
            type = "app";
            program = "${pkg}/bin/${pkg.meta.mainProgram}";
          };
          fetch-deps = {
            type = "app";
            program = toString self.packages.${system}.final-frontier-launcher.passthru.fetch-deps;
          };
        }
      );

      formatter = forAllSystems (_: pkgs: pkgs.nixpkgs-fmt);

    };
}
