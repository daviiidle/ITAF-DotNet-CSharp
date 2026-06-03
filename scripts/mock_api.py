from http.server import BaseHTTPRequestHandler, HTTPServer
import json


class Handler(BaseHTTPRequestHandler):
    def do_GET(self):
        if self.path == "/health":
            self._json(200, {"status": "ok"})
            return

        if self.path == "/posts/1":
            self._json(
                200,
                {
                    "userId": 1,
                    "id": 1,
                    "title": "deterministic post",
                    "body": "served by ITAF CI mock API",
                },
            )
            return

        self._json(404, {"error": "not found"})

    def do_POST(self):
        if self.path != "/posts":
            self._json(404, {"error": "not found"})
            return

        length = int(self.headers.get("Content-Length", "0"))
        payload = json.loads(self.rfile.read(length) or b"{}")
        payload["id"] = 101
        self._json(201, payload)

    def log_message(self, format, *args):
        return

    def _json(self, status, payload):
        body = json.dumps(payload).encode("utf-8")
        self.send_response(status)
        self.send_header("Content-Type", "application/json")
        self.send_header("Content-Length", str(len(body)))
        self.end_headers()
        self.wfile.write(body)


if __name__ == "__main__":
    HTTPServer(("0.0.0.0", 4020), Handler).serve_forever()

