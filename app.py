from flask import Flask

app = Flask(__name__)

@app.route('/')
def hello():
    return 'Helloooo, worssssssssdldss!!ss!!23'

if __name__ == '__main__':
    app.run()
