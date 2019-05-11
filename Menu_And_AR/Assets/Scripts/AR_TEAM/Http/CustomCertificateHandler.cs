using UnityEngine.Networking;

namespace Assets.Scripts.AR_TEAM.Http {
    public class CustomCertificateHandler : CertificateHandler {
        protected override bool ValidateCertificate(byte[] certificateData) {
            return true;
        }
    }
}
